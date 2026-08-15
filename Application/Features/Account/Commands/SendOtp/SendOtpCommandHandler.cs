using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Account.Commands.SendOtp;

public class SendOtpCommandHandler : IRequestHandler<SendOtpCommand>
{
    private readonly IIdentityService _identityService;
    private readonly INotificationService _notificationService;

    public SendOtpCommandHandler(
        IIdentityService identityService,
        INotificationService notificationService)
    {
        _identityService = identityService;
        _notificationService = notificationService;
    }

    public async Task Handle(
        SendOtpCommand request,
        CancellationToken cancellationToken)
    {
        await _identityService.EnsureUserExistsAsync(request.Email);

        if (await _identityService.IsBlockedAsync(request.Email))
        {
            throw new InvalidOperationException(
                "Your account has been blocked.");
        }

        var otp = await _identityService.GenerateOtpAsync(request.Email);

        await _notificationService.SendOtpAsync(
            request.Email,
            otp);
    }
}