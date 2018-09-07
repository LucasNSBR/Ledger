using Ledger.CrossCutting.Identity.Abstractions;
using Ledger.CrossCutting.Identity.AppServices.UserAppServices;
using Ledger.CrossCutting.Identity.Configuration;
using Ledger.CrossCutting.Identity.Context;
using Ledger.CrossCutting.Identity.Models.Roles;
using Ledger.CrossCutting.Identity.Models.Services;
using Ledger.CrossCutting.Identity.Models.Users;
using Ledger.CrossCutting.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ledger.CrossCutting.IoC
{
    public static class IdentityBootstrapper
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LedgerIdentityDbContext>(options =>
                options.UseInMemoryDatabase("IdentityDb"));

            InitializeIdentity(services);
            InitializeJwtConfiguration(services, configuration);
            InitializeApplicationServices(services);
        }

        private static void InitializeIdentity(IServiceCollection services)
        {
            services.AddIdentity<LedgerIdentityUser, LedgerIdentityRole>(cfg =>
            {
                cfg.User = new UserOptions
                {
                    AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@",
                    RequireUniqueEmail = true
                };

                cfg.Password = new PasswordOptions
                {
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false,
                    RequireDigit = false,
                    RequiredUniqueChars = 0,
                    RequiredLength = 8,
                };

                cfg.SignIn = new SignInOptions
                {
                    RequireConfirmedEmail = false,
                    RequireConfirmedPhoneNumber = false,
                };

                cfg.Lockout = new LockoutOptions
                {
                    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5),
                    MaxFailedAccessAttempts = 10
                };
            })
            .AddEntityFrameworkStores<LedgerIdentityDbContext>()
            .AddUserManager<LedgerUserManager>()
            .AddSignInManager<LedgerSignInManager>()
            .AddDefaultTokenProviders();
        }

        //TODO: REFACTORE THIS
        private static void InitializeJwtConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtTokenOptions>(cfg =>
            {
                cfg.Issuer = configuration["JwtToken:Issuer"];
                cfg.Audience = configuration["JwtToken:Issuer"];
                cfg.ExpiresInSeconds = configuration.GetValue<int>("JwtToken:ExpiresInSeconds");
                cfg.IssuedAt = DateTime.Now;
                cfg.NotBefore = DateTime.Now;
            });

            services.AddSingleton<ISigningConfiguration, SigningConfiguration>();
            services.AddScoped<IJwtFactory, JwtFactory>();
        }

        private static void InitializeApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IUserApplicationService, UserApplicationService>();
        }
    }
}
