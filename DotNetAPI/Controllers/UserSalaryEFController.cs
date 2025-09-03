using AutoMapper;
using DotNetAPI.Data;
using DotNetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserSalaryEFController(IConfiguration config) : ControllerBase
{
    readonly DataContextEF _ef = new(config);

    [HttpGet("GetAllSalaries")]
    public IEnumerable<UserSalary> GetAllSalaries()
    {
        IEnumerable<UserSalary> userSalaries = _ef.UserSalary.ToList();

        return userSalaries;
    }

    [HttpGet("GetUserSalary/{userId}")]
    public UserSalary GetUserSalary(int userId)
    {
        UserSalary? salary = _ef.UserSalary
                                            .Where(uS => uS.UserId == userId)
                                            .FirstOrDefault();
        
        if(salary != null)
        {
            return salary;    
        }

        throw new Exception("Unable to fetch user salary!");
    }

    [HttpPut("EditUserSalary/")]
    public IActionResult EditUserSalary(UserSalary userSalary)
    {
        UserSalary? salaryInfoDB = _ef.UserSalary
                                                  .Where(us => us.UserId == userSalary.UserId)
                                                  .FirstOrDefault();

        if(salaryInfoDB != null)
        {
            salaryInfoDB.Salary = userSalary.Salary;
            salaryInfoDB.AvgSalary = userSalary.AvgSalary;

            if(_ef.SaveChanges() > 0)
            {
                return Ok();
            }
        }
        
        throw new Exception("Unable to edit user's salary info!");
    }

    [HttpPost("AddUserSalary/")]
    public IActionResult AddUserSalary(UserSalary userSalary)
    {
        _ef.Add(userSalary);

        if(_ef.SaveChanges() > 0)
        {
            return Created();
        }

        throw new Exception("Unable to add user salary info!");
    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        UserSalary? salaryDB = _ef.UserSalary
                                  .Where(us => us.UserId == userId)
                                  .FirstOrDefault();

        if(salaryDB != null)
        {
            _ef.Remove(salaryDB);

            if(_ef.SaveChanges() > 0)
            {
                return Ok();
            }
        }

        throw new Exception("Unable to delete user's salary info!");
    }
}