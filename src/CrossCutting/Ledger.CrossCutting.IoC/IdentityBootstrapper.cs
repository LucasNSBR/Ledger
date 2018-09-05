using Ledger.CrossCutting.Identity.Context;
using Ledger.CrossCutting.Identity.Models.Managers;
using Ledger.CrossCutting.Identity.Models.Roles;
using Ledger.CrossCutting.Identity.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ledger.CrossCutting.IoC
{
    public static class IdentityBootstrapper
    {
        public static void Initialize(IServiceCollection services)
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
            .AddDefaultTokenProviders();
        }
    }
}
