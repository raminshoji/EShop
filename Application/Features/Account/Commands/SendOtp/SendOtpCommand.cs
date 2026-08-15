using Application.Features.Account.Commands.VerifyOtp;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Account.Commands.SendOtp
{
    public sealed record SendOtpCommand(string Email) : IRequest;

}
