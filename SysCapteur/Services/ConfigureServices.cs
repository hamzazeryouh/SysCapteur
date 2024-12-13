using Autofac.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sys.Application;
using Sys.Application.DTO.Auth;
using Sys.Application.Interfaces;
using Sys.Domain.Entities.Users;
using Sys.Presistence.DataAccess;
using Sys.Presistence.DataContext;
using Sys.Presistence.Repository;
using Sys.Presistence.Repository.Auth;
using Sys.Presistence.Services;
using Sys.Presistence.Services.AuthService;
using System.Runtime.CompilerServices;
using System.Text;

namespace SysCapteur.Services
{
    public static class ConfigureServices
    {

        public static void ConfigureAuth(this IHostApplicationBuilder Builder)
        {
            var jwtSettings = Builder.Configuration.GetSection("Jwt");
            var secretKey = jwtSettings["SecretKey"]!;
            var issuer = jwtSettings["Issuer"]!;
            var audience = jwtSettings["Audience"]!;
            var expirationMinutes = int.Parse(jwtSettings["ExpirationMinutes"]!);
            Builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
         
            
            // Configurer l'authentification JWT
            Builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = issuer,
                        ValidAudience = audience
                    };
                });
            // Ajouter AuthService and repo comme singleton
            Builder.Services.Configure<JwtSettings>(Builder.Configuration.GetSection("JWT"));
            Builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // Register UnitOfWork
            Builder.Services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            Builder.Services.AddScoped<ISensorRepository, SensorRepository>();
            Builder.Services.AddScoped<ISensorService, SensorService>();
            Builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            Builder.Services.AddScoped<IAuthService, AuthService>();
        }

    }
}
