using Application.Features.Account.Commands.SendOtp;
using Application.Features.Account.Commands.VerifyOtp;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ISender _sender;

    public AccountController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("send-otp")]
    public async Task<IActionResult> SendOtp(
        SendOtpCommand command)
    {
        await _sender.Send(command);

        return Ok();
    }

    [HttpPost("verify-otp")]
    [ProducesResponseType(typeof(VerifyOtpResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> VerifyOtp(VerifyOtpCommand command)
    {
        var response = await _sender.Send(command);

        return Ok(response);
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok("You are authenticated.");
    }
}