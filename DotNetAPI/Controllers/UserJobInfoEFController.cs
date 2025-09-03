using AutoMapper;
using DotNetAPI.Data;
using DotNetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserJobInfoEFController(IConfiguration config) : ControllerBase
{
    readonly DataContextEF _ef = new(config);

    [HttpGet("GetAllJobInfos")]
    public IEnumerable<UserJobInfo> GetAllJobInfos()
    {
        IEnumerable<UserJobInfo> jobInfos = _ef.UserJobInfo.ToList();

        return jobInfos;
    }

    [HttpGet("GetUserJobInfo/{userId}")]
    public UserJobInfo GetUserJobInfo(int userId)
    {
        UserJobInfo? jobInfo = _ef.UserJobInfo
                                  .Where(uj => uj.UserId == userId)
                                  .FirstOrDefault();
        
        if(jobInfo != null)
        {
            return jobInfo;    
        }

        throw new Exception("Unable to fetch user job info!");
    }

    [HttpPut("EditUserJobInfo/")]
    public IActionResult EditUserJobInfo(UserJobInfo UserJobInfo)
    {
        UserJobInfo? jobInfoDB = _ef.UserJobInfo
                                    .Where(uj => uj.UserId == UserJobInfo.UserId)
                                    .FirstOrDefault();

        if(jobInfoDB != null)
        {
            jobInfoDB.Department = UserJobInfo.Department;
            jobInfoDB.JobTitle = UserJobInfo.JobTitle;

            if(_ef.SaveChanges() > 0)
            {
                return Ok();
            }
        }
        
        throw new Exception("Unable to edit user's job info!");
    }

    [HttpPost("AddUserJobInfo/")]
    public IActionResult AddUserJobInfo(UserJobInfo UserJobInfo)
    {
        _ef.Add(UserJobInfo);

        if(_ef.SaveChanges() > 0)
        {
            return Created();
        }

        throw new Exception("Unable to add user job info!");
    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        UserJobInfo? jobInfoDB = _ef.UserJobInfo
                                    .Where(uj => uj.UserId == userId)
                                    .FirstOrDefault();

        if(jobInfoDB != null)
        {
            _ef.Remove(jobInfoDB);

            if(_ef.SaveChanges() > 0)
            {
                return Ok();
            }
        }

        throw new Exception("Unable to delete user's job info!");
    }
}