using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class ErrorController : ControllerBase
{
    [HttpGet("not-found")]
    public IActionResult ErrorNotFound()
    {
        return NotFound();
    }

    [HttpGet("bad-request")]
    public IActionResult ErrorBadRequest()
    {
        return BadRequest();
    }

    [HttpGet("unauthorized")]
    public IActionResult ErrorUnauthorized()
    {
        return Unauthorized();
    }

    [HttpGet("server-error")]
    public IActionResult ErrorServerError()
    {
        throw new Exception("Server error");
    }

    [HttpGet("validation-error")]
    public IActionResult ErrorValidationError()
    {
        ModelState.AddModelError("Name", "Name is required");
        ModelState.AddModelError("Price", "Price is required");
        return ValidationProblem();
    }
}