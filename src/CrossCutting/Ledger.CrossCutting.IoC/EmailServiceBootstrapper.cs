using Ledger.CrossCutting.EmailService.Configuration;
using Ledger.CrossCutting.EmailService.Dispatchers;
using Ledger.CrossCutting.EmailService.Services.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ledger.CrossCutting.IoC
{
    public static class EmailServiceBootstrapper
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DispatcherOptions>(cfg =>
            {
                cfg.SenderName = configuration["SendGrid:SenderName"];
                cfg.SendAddress = configuration["SendGrid:SenderEmail"];
                cfg.SendGridKey = configuration["SendGrid:API_KEY"];
                cfg.SendGridUser = configuration["SendGrid:USER"];
            });

            services.Configure<TemplateOptions>(cfg =>
            {
                cfg.ConfirmUserAccountEmailTemplateId = configuration["SendGrid:Templates:ConfirmUserAccountTemplate"];
                cfg.ResetUserPasswordEmailTemplateId = configuration["SendGrid:Templates:ResetUserPasswordTemplate"];
            });

            services.AddScoped<IEmailFactory, EmailFactory>();
            services.AddScoped<IEmailDispatcher, EmailDispatcher>();
        }
    }
}
