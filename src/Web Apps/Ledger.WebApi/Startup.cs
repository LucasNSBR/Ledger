using Ledger.CrossCutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Ledger.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Bootstrapper.Initialize(services);

            services
                   .AddMvc()
                   .AddJsonOptions(options =>
                   {
                       options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                       options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                   });

            services.AddResponseCompression();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseResponseCompression();
            app.UseMvc();
        }
    }
}
