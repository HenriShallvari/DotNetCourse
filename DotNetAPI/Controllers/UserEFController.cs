using DotNetAPI.Data;
using DotNetAPI.DTOs;
using DotNetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController(IConfiguration config) : ControllerBase
{

    // readonly DataContextDapper _dapper = new(config);
    readonly DataContextEF _entityFramework = new(config);

    [HttpGet("GetAllUsers")]
    public IEnumerable<User> GetAllUsers()
    {
        IEnumerable<User> users = _entityFramework.Users.ToList<User>();
        return users;
    }

    [HttpGet("GetUser/{userID}")]
    public User GetUser(int userID)
    {
        User? user = _entityFramework.Users
                                    .Where(u => u.UserId == userID)
                                    .FirstOrDefault<User>();

        if(user != null)
        {
            return user; 
        }

        throw new Exception("No user found for this ID!");
    }

    [HttpPut("EditUser/")]
    public IActionResult EditUser(User user)
    {
        User? userDB = _entityFramework.Users
                            .Where(u => u.UserId == user.UserId)
                            .FirstOrDefault<User>();

        if(userDB != null)
        {
            userDB.Active = user.Active;
            userDB.FirstName = user.FirstName;
            userDB.LastName = user.LastName;
            userDB.Email = user.Email;
            userDB.Gender = user.Gender;

            if(_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
        }

        throw new Exception("Failed to update user!");
    }

    [HttpPost("AddUser/")]
    public IActionResult AddUser(UserToAddDTO user)
    {
        // NOT Great: we can use a library like AutoMapper to simplify this.
        User userToAdd = new()
        { 
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Gender = user.Gender,
            Active = user.Active
        };

        _entityFramework.Add(userToAdd);

        if(_entityFramework.SaveChanges() > 0)
        {
            return Created();
        }
            

        throw new Exception("Failed to insert user!");
    }

    [HttpDelete("DeleteUser/{userID}")]
    public IActionResult DeleteUser(int userID)
    {

        User? userDB = _entityFramework.Users
                                    .Where(u => u.UserId == userID)
                                    .FirstOrDefault();

        if(userDB != null)
        {
            _entityFramework.Remove(userDB);

            if(_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }   
        }
        
        throw new Exception("Failed to delete user!");
    }
}