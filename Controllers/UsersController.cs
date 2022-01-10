using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;

namespace ODataActionBug.Controllers;

public class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult Hi()
    {
        return Ok(
            new Hello
            {
                Message = "Hi!"
            }
        );
    }

    [HttpPost]
    public IActionResult HiSimple(string name)
    {
        return Ok(
            new Hello
            {
                Message = $"Hi {name}!"
            }
        );
    }

    [HttpPost]
    public IActionResult HiSimpleFunction(string name)
    {
        return Ok(
            new Hello
            {
                Message = $"Hi {name}!"
            }
        );
    }

    [HttpPost]
    public IActionResult Hello(
        [FromODataBody]SayHello request
        // The below is needed otherwise this method is not picked up for some reason
        // , ODataActionParameters parameters
    )
    {
        return Ok(
            new Hello
            {
                Message = $"Hi {request.FirstName} {request.LastName}!"
            }
        );
    }

    [HttpPost]
    public IActionResult Goodbye(
        int key,
        [FromODataBody]SayHello request
        // The below is needed otherwise this method is not picked up for some reason
        // , ODataActionParameters parameters
    )
    {
        return Ok(
            new Hello
            {
                Message = $"Hi {request.FirstName} {request.LastName}!"
            }
        );
    }

}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class SayHello
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class Hello
{
    public string Message { get; set; }
}
