using Ledger.Identity.Application.AppServices.UserAppServices;
using Ledger.Identity.Data.Context;
using Ledger.Identity.Domain.Aggregates.RoleAggregate;
using Ledger.Identity.Domain.Aggregates.UserAggregate;
using Ledger.Identity.Domain.Configuration.JwtConfigurations;
using Ledger.Identity.Domain.Configuration.SigningConfigurations;
using Ledger.Identity.Domain.EventHandlers.UserAggregate;
using Ledger.Identity.Domain.Events.UserEvents;
using Ledger.Identity.Domain.Models.Services.UserServices;
using Ledger.Identity.Domain.Services;
using Ledger.Identity.Domain.Services.SigningServices;
using Ledger.Shared.EventHandlers;
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
            InitializeDomainEventHandlers(services);
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

            services.Configure<SigningOptions>(cfg =>
            {
                cfg.SALT_KEY = configuration["SALT_KEY"];
            });

            services.AddSingleton<ISigningService, SigningService>();
            services.AddScoped<IJwtFactory, JwtFactory>();
        }

        private static void InitializeApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IUserApplicationService, UserApplicationService>();
        }

        private static void InitializeDomainEventHandlers(IServiceCollection services)
        {
            services.AddScoped<IDomainEventHandler<UserRegisteredEvent>, UserDomainEventHandler>();
            services.AddScoped<IDomainEventHandler<UserLoggedInEvent>, UserDomainEventHandler>();
            services.AddScoped<IDomainEventHandler<UserForgotPasswordEvent>, UserDomainEventHandler>();
            services.AddScoped<IDomainEventHandler<UserResetedPasswordEvent>, UserDomainEventHandler>();
            services.AddScoped<IDomainEventHandler<UserChangedPasswordEvent>, UserDomainEventHandler>();
        }
    }
}
