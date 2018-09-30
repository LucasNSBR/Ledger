using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.IoC.Configuration;
using Ledger.HelpDesk.Application.AppServices.TicketAppServices;
using Ledger.HelpDesk.Application.AppServices.TicketCategoryAppServices;
using Ledger.HelpDesk.Data.Context;
using Ledger.HelpDesk.Data.Repositories.RoleRepositories;
using Ledger.HelpDesk.Data.Repositories.TicketCategoryRepositories;
using Ledger.HelpDesk.Data.Repositories.TicketRepositories;
using Ledger.HelpDesk.Data.Repositories.UserRepositories;
using Ledger.HelpDesk.Domain.Context;
using Ledger.HelpDesk.Domain.Factories;
using Ledger.HelpDesk.Domain.Repositories.RoleRepositories;
using Ledger.HelpDesk.Domain.Repositories.TicketCategoryRepositories;
using Ledger.HelpDesk.Domain.Repositories.TicketRepositories;
using Ledger.HelpDesk.Domain.Repositories.UserRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ledger.CrossCutting.IoC
{
    public static partial class DependencyInjectionExtensions
    {
        /// <summary>
        /// Add Dependencies for Help Desk Bounded Context
        /// </summary>
        /// <param name="services">List of services to register</param>
        /// <param name="setupAction">Configuration to start context</param>
        /// <returns></returns>
        public static IServiceCollection AddHelpDesk(this IServiceCollection services, Action<HelpDeskContextOptions> setupAction = null)
        {
            services.AddDbContext<LedgerHelpDeskDbContext>(options =>
                  options.UseInMemoryDatabase("HelpDeskDb"));

            services.AddScoped<ITicketFactory, TicketFactory>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketCategoryRepository, TicketCategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITicketApplicationService, TicketApplicationService>();
            services.AddScoped<ITicketCategoryApplicationService, TicketCategoryApplicationService>();

            services.AddScoped<ILedgerHelpDeskDbAbstraction, LedgerHelpDeskDbContext>(provider => provider.GetRequiredService<LedgerHelpDeskDbContext>());
            services.AddScoped<IUnitOfWork<ILedgerHelpDeskDbAbstraction>, UnitOfWork<ILedgerHelpDeskDbAbstraction>>();

            return services;
        }
    }
}
