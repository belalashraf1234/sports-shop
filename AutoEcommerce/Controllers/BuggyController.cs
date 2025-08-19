using Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AutoEcommerce.Controllers;

public class BuggyController : BaseApiController{
    
    [HttpGet("unauthorized")]
    public IActionResult GetUnauthorized()
    {   
        return Unauthorized();
    }
    [HttpGet("badrequest")]
    public IActionResult GetBadRequest()
    {   
        return BadRequest("this is bad request");
    }
    [HttpGet("notfound")]
    public IActionResult GetNotFound()
    {
        return NotFound();
    }
    [HttpGet("internalerror")]
    public IActionResult GetInternalError()
    {   
          throw new Exception("This is test exception error");
    }
    [HttpPost("validationerror")]
    public IActionResult GetvalidationError(DemoProduct product)
    {
        return Ok();
    }
}