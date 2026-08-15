using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Commands.VerifyOtp
{
    public sealed record VerifyOtpCommand(string Email,string Code) : IRequest<string>;
}
