using Ledger.Shared.Locations.Context;
using Ledger.Shared.Locations.Repositories.CityRepositories;
using Ledger.Shared.Locations.Repositories.CountryRepositories;
using Ledger.Shared.Locations.Repositories.StateRepositories;
using Ledger.Shared.Locations.Services;
using Ledger.Shared.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ledger.CrossCutting.IoC
{
    public static partial class DependencyInjectionExtensions
    {
        /// <summary>
        /// Add Dependencies for Shared Kernel
        /// </summary>
        /// <param name="services">List of services to register</param>
        /// <returns></returns>
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddDbContext<LedgerLocationDbContext>(options =>
                  options.UseInMemoryDatabase("LocationDb"));

            services.AddScoped<IDomainNotificationHandler, DomainNotificationHandler>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ILocationService, LocationService>();

            return services;
        }
    }
}
