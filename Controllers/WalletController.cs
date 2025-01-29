using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletAPI.DTOs;
using WalletAPI.Services;

namespace WalletAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class WalletController : ControllerBase
{
    private readonly WalletServices _walletServices;

    public WalletController(WalletServices walletServices)
    {
        _walletServices = walletServices;
    }

    [Authorize]
    [HttpGet("balance")]
    public IActionResult GetBalance()
    {
        try
        {
            var userId = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var balance = _walletServices.GetBalance(userId);
            return Ok(new {balance = balance});
        }
        catch (Exception e)
        {
            return BadRequest(new {error = e.Message});
        }
    }

    [Authorize]
    [HttpPost("add-balance")]
    public IActionResult AddBalance([FromBody] AddBalanceDTO dto)
    {
        try
        {
            if(dto == null || dto.Amount <= 0)
                return BadRequest(new {message = "Invalid amount"});
            
            var userId = Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            _walletServices.AddBalance(userId, dto.Amount);
            
            return Ok(new {message = "Success"});
        }
        catch (Exception e)
        {
            return BadRequest(new {message = e.Message});
        }
    }
}