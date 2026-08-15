using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Account.Commands.VerifyOtp;

public class VerifyOtpCommandHandler
    : IRequestHandler<VerifyOtpCommand, string>
{
    private readonly IIdentityService _identityService;
    private readonly IJwtService _jwtService;

    public VerifyOtpCommandHandler(
        IIdentityService identityService,
        IJwtService jwtService)
    {
        _identityService = identityService;
        _jwtService = jwtService;
    }

    public async Task<string> Handle(
        VerifyOtpCommand request,
        CancellationToken cancellationToken)
    {
        var userId = await _identityService.VerifyOtpAsync(
            request.Email,
            request.Code);

        var token = _jwtService.GenerateToken(
            userId,
            request.Email);

        return token;
    }
}