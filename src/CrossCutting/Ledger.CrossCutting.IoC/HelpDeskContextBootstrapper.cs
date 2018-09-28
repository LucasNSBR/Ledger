using Ledger.HelpDesk.Application.AppServices.TicketAppServices;
using Ledger.HelpDesk.Application.AppServices.TicketCategoryAppServices;
using Ledger.HelpDesk.Data.Context;
using Ledger.HelpDesk.Data.Repositories.RoleRepositories;
using Ledger.HelpDesk.Data.Repositories.TicketCategoryRepositories;
using Ledger.HelpDesk.Data.Repositories.TicketRepositories;
using Ledger.HelpDesk.Domain.Repositories.RoleRepositories;
using Ledger.HelpDesk.Domain.Repositories.TicketCategoryRepositories;
using Ledger.HelpDesk.Domain.Repositories.TicketRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ledger.CrossCutting.IoC
{
    public static class HelpDeskContextBootstrapper
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddDbContext<LedgerHelpDeskDbContext>(options =>
                options.UseInMemoryDatabase("HelpDeskDb"));

            InitializeRepositories(services);
            InitializeApplicationServices(services);
        }
        
        private static void InitializeRepositories(IServiceCollection services)
        {
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketCategoryRepository, TicketCategoryRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
        }
        
        private static void InitializeApplicationServices(IServiceCollection services)
        {
            services.AddScoped<ITicketApplicationService, TicketApplicationService>();
            services.AddScoped<ITicketCategoryApplicationService, TicketCategoryApplicationService>();
        }
    }
}
