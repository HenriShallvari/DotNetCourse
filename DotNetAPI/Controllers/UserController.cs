using DotNetAPI.Data;
using DotNetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IConfiguration config) : ControllerBase
{

    readonly DataContextDapper _dapper = new(config);

    [HttpGet("GetAllUsers")]
    // public IActionResult Test() basically a safe way to say you're going to return anything
    public IEnumerable<User> GetAllUsers()
    {
        string sql = "SELECT * FROM TutorialAppSchema.USERS";
        IEnumerable<User> users = _dapper.LoadData<User>(sql);
        return users;
    }

    [HttpGet("GetUser/{userID}")]
    public User GetUser(int userID)
    {
        // string sql = "SELECT * FROM TutorialAppSchema.USERS WHERE UserID = '" + userID.ToString() + "'";
        string sql = "SELECT * FROM TutorialAppSchema.USERS WHERE UserID = @UserID";
        var parameters = new { UserId = userID };
        User user = _dapper.LoadDataSingle<User>(sql, parameters);

        return user; 
    }

    [HttpPut("EditUser/")]
    public IActionResult EditUser(User user)
    {
        string sql = @"UPDATE TutorialAppSchema.USERS 
                          SET FirstName = @FirstName,
                              LastName = @LastName,
                              Email = @Email,
                              Gender = @Gender,
                              Active = @Active
                        WHERE UserId = @UserId";
        var parameters = new
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Gender = user.Gender,
            Active = user.Active,
            UserId = user.UserId
        };

        if(_dapper.ExecuteSql(sql, parameters))
        {
            return Ok();
        }

        throw new Exception("Failed to update user!");
    }

    [HttpPost("AddUser/")]
    public IActionResult AddUser(User user)
    {
        string sql = @"INSERT INTO TutorialAppSchema.USERS(
                          FirstName,
                          LastName,
                          Email,
                          Gender,
                          Active
                       ) VALUES (
                          @FirstName,
                          @LastName,
                          @Email,
                          @Gender,
                          @Active
                       )";
        var parameters = new
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Gender = user.Gender,
            Active = user.Active
        };

        if(_dapper.ExecuteSql(sql, parameters))
        {
            return Created();
        }
            

        throw new Exception("Failed to insert user!");
    }
}