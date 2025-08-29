using DotNetAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    DataContextDapper _dapper;

    public UserController(IConfiguration config)
    {
        // Console.WriteLine(config.GetConnectionString("DefaultConnection"));
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUsers/{testValue}")]
    // public IActionResult Test() basically a safe way to say you're going to return anything
    public string[] GetUsers(string testValue)
    {
        string[] respArray = [ 
            "test1",
            "test2",
            testValue
        ];

        return respArray;
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }
}