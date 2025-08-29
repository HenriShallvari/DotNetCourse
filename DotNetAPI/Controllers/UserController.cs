using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    public UserController()
    {

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
}