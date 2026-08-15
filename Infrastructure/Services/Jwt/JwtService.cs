using Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtService(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }

        public string GenerateToken(long userId, string email)
        {
            var claims = new List<Claim>
{
    new(ClaimTypes.NameIdentifier, userId.ToString()),
    new(ClaimTypes.Email, email)
};

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
             issuer: _jwtOptions.Issuer,
             audience: _jwtOptions.Audience,
             claims: claims,
             expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinutes),
             signingCredentials: credentials);

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}
