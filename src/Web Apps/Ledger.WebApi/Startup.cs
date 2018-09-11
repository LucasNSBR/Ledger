using Ledger.CrossCutting.IoC;
using Ledger.Identity.Domain.Services.SigningServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;

namespace Ledger.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IServiceProvider Provider { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Bootstrapper.Initialize(services, Configuration);

            services
                .AddAuthentication(cfg =>
                {
                    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = Configuration["JwtToken:Issuer"],
                        ValidAudience = Configuration["JwtToken:Audience"],
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = services.BuildServiceProvider().GetRequiredService<ISigningService>().SecurityKey 
                    };
                });

            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("ActivatedAccount", cfgPolicy =>
                {
                    cfgPolicy.RequireClaim("activated-account", "true");
                });
            });

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
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
