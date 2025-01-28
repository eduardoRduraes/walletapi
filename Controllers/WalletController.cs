using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WalletAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class WalletController : ControllerBase
{
    [Authorize]
    [HttpGet("protected")]
    public IActionResult ProtectedEndpoint()
    {
        return Ok(new {message = "Protected endpoint"});
    }
}