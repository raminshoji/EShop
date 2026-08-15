using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task EnsureUserExistsAsync(string email);

        Task<bool> IsBlockedAsync(string email);

        Task<string> GenerateOtpAsync(string email);

        Task<long> VerifyOtpAsync(string email, string code);
    }
}

