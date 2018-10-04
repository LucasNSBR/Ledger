using Ledger.CrossCutting.IoC;
using Ledger.Identity.Domain.Configuration.JwtConfigurations;
using Ledger.Identity.Domain.Configuration.SigningConfigurations;
using Ledger.Identity.Domain.Services;
using Ledger.Identity.Domain.Services.SigningServices;
using Ledger.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
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
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCore();
            services.AddIdentity();
            services.AddActivations();
            services.AddCompanies();
            services.AddHelpDesk();

            services.AddServiceBus(opt =>
            {
                opt.HostAddress = Configuration["MassTransit:RabbitMqHost"];
                opt.RabbitMqHostUser = Configuration["MassTransit:RabbitMqHostUser"];
                opt.RabbitMqHostPassword = Configuration["MassTransit:RabbitMqHostPassword"];
            });

            services.AddEmailService(opt =>
            {
                opt.SenderName = Configuration["SendGrid:SenderName"];
                opt.SendAddress = Configuration["SendGrid:SenderEmail"];
                opt.SendGridKey = Configuration["SendGrid:API_KEY"];
            });

            services.Configure<JwtTokenOptions>(cfg =>
            {
                cfg.Issuer = Configuration["JwtToken:Issuer"];
                cfg.Audience = Configuration["JwtToken:Issuer"];
                cfg.ExpiresInSeconds = Configuration.GetValue<int>("JwtToken:ExpiresInSeconds");
                cfg.IssuedAt = DateTime.Now;
                cfg.NotBefore = DateTime.Now;
            });

            services.Configure<SigningOptions>(cfg =>
            {
                cfg.SALT_KEY = Configuration["SALT_KEY"];
            });

            services.AddSingleton<ISigningService, SigningService>();
            services.AddScoped<IJwtFactory, JwtFactory>();

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

                cfg.AddPolicy("AdminAccount", cfgPolicy =>
                {
                    cfgPolicy.RequireRole("admin-account");
                });

                cfg.AddPolicy("SupportAccount", cfgPolicy =>
                {
                    cfgPolicy.RequireRole("support-account");
                });
            });

            services
                .AddSwaggerGen(cfg =>
                {
                    cfg.SwaggerDoc("v1", new Info
                    {
                        Title = "Ledger v1",
                        Contact = new Contact
                        {
                            Name = "Lucas Campos",
                            Url = "http://github.com/lucasnsbr"
                        },
                        Version = "v1",
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

            app.UseExceptionLogger();
            app.UseResponseCompression();
            app.UseAuthentication();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint(Configuration["SwaggerEndpoint"], "Ledger v1");
            });
        }
    }
}
