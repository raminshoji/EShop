using Application.Common.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Services.Jwt;
using Infrastructure.Services.Notification;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<INotificationService, EmailNotificationService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUser, CurrentUser>();
            services.AddIdentityCore<ApplicationUser>()
           .AddRoles<IdentityRole<long>>()
           .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

            var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()!;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });
            services.AddScoped<IJwtService, JwtService>();
            services.Configure<EmailOptions>(configuration.GetSection(EmailOptions.SectionName));

            return services;



        }
    }
}
