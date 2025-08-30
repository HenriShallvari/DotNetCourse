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
    // public IActionResult Test() basically a safe way to say you're going to return anything
    public User GetUser(int userID)
    {
        // string sql = "SELECT * FROM TutorialAppSchema.USERS WHERE UserID = '" + userID.ToString() + "'";
        string sql = "SELECT * FROM TutorialAppSchema.USERS WHERE UserID = @UserID";
        var parameters = new { UserId = userID };
        User user = _dapper.LoadDataSingle<User>(sql, parameters);

        return user; 
    }
}