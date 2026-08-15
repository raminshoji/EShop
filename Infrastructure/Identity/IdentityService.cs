using Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task EnsureUserExistsAsync(string email)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.Email == email);

            if (user is not null)
                return;

            var result = await _userManager.CreateAsync(new ApplicationUser
            {
                UserName = email,
                Email = email
            });

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    string.Join(", ", result.Errors.Select(x => x.Description)));
            }
        }

        public async Task<string> GenerateOtpAsync(string email)
        {
            var user = await _userManager.Users
                .SingleAsync(x => x.Email == email);

            var otp = RandomNumberGenerator
                .GetInt32(10000, 99999)
                .ToString();

            user.OtpCode = otp;
            user.OtpExpireTime = DateTime.UtcNow.AddMinutes(2);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    "Failed to generate OTP.");
            }

            return otp;
        }

        public async Task<bool> IsBlockedAsync(string email)
        {
            var user = await _userManager.Users
                .SingleAsync(x => x.Email == email);

            return user.IsBlocked;
        }

        public async Task<long> VerifyOtpAsync(
      string email,
      string code)
        {
            var user = await _userManager.Users
                .SingleAsync(x => x.Email == email);

            if (user.OtpExpireTime is null ||
                user.OtpExpireTime < DateTime.UtcNow)
            {
                throw new InvalidOperationException(
                    "OTP has expired.");
            }

            if (user.OtpCode != code)
            {
                throw new InvalidOperationException(
                    "OTP is invalid.");
            }

            user.EmailConfirmed = true;

            user.OtpCode = null;
            user.OtpExpireTime = null;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    "Failed to verify OTP.");
            }

            return user.Id;
        }
    }
}
