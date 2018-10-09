using Ledger.CrossCutting.IoC.Configuration;
using Ledger.Identity.Application.AppServices.RoleAppServices;
using Ledger.Identity.Application.AppServices.UserAppServices;
using Ledger.Identity.Data.Context;
using Ledger.Identity.Domain.Aggregates.RoleAggregate;
using Ledger.Identity.Domain.Aggregates.UserAggregate;
using Ledger.Identity.Domain.EventHandlers.UserAggregate;
using Ledger.Identity.Domain.Events.UserEvents;
using Ledger.Identity.Domain.Models.Services.UserServices;
using Ledger.Identity.Domain.Services.RoleServices;
using Ledger.Identity.Domain.Services.UserServices.UserResolver;
using Ledger.Shared.EventHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ledger.CrossCutting.IoC
{
    public static partial class DependencyInjectionExtensions
    {
        /// <summary>
        /// Add Dependencies for Identity Bounded Context
        /// </summary>
        /// <param name="services">List of services to register</param>
        /// <param name="setupAction">Optional configurations to setup context</param>
        /// <returns></returns>
        public static IServiceCollection AddIdentity(this IServiceCollection services, Action<IdentityContextOptions> setupAction = null)
        {
            services.AddDbContext<LedgerIdentityDbContext>(options =>
                options.UseInMemoryDatabase("IdentityDb"));

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
            .AddRoleManager<LedgerRoleManager>()
            .AddDefaultTokenProviders();

            services.AddScoped<IUserApplicationService, UserApplicationService>();
            services.AddScoped<IRoleApplicationService, RoleApplicationService>();

            services.AddScoped<IDomainEventHandler<UserRegisteredEvent>, UserDomainEventHandler>();
            services.AddScoped<IDomainEventHandler<UserLoggedInEvent>, UserDomainEventHandler>();
            services.AddScoped<IDomainEventHandler<UserForgotPasswordEvent>, UserDomainEventHandler>();
            services.AddScoped<IDomainEventHandler<UserResetedPasswordEvent>, UserDomainEventHandler>();
            services.AddScoped<IDomainEventHandler<UserChangedPasswordEvent>, UserDomainEventHandler>();

            services.AddScoped<IIdentityResolverService, IdentityResolverService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}
