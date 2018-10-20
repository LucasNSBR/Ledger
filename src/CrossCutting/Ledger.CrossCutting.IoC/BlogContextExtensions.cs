using Ledger.Blog.Application.AppServices.ArticleAppServices;
using Ledger.Blog.Application.AppServices.ArticleCategoryAppServices;
using Ledger.Blog.Data.Context;
using Ledger.Blog.Data.Repositories.ArticleCategoryRepositories;
using Ledger.Blog.Data.Repositories.ArticleRepositories;
using Ledger.Blog.Domain.Context;
using Ledger.Blog.Domain.Repositories.ArticleCategoryRepositories;
using Ledger.Blog.Domain.Repositories.ArticleRepositories;
using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.IoC.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ledger.CrossCutting.IoC
{
    public static partial class DependencyInjectionExtensions
    {
        /// <summary>
        /// Add Dependencies for Blogging Bounded Context
        /// </summary>
        /// <param name="services">List of services to register</param>
        /// <param name="options">Optional configurations to setup context</param>
        /// <returns></returns>
        public static IServiceCollection AddBlogging(this IServiceCollection services, Action<BlogContextOptions> setupAction = null)
        {
            services.AddDbContext<LedgerBlogDbContext>(options =>
                  options.UseInMemoryDatabase("BlogDb"));

            services.AddScoped<IArticleApplicationService, ArticleApplicationService>();
            services.AddScoped<IArticleCategoryApplicationService, ArticleCategoryApplicationService>();

            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();

            services.AddScoped<ILedgerBlogDbAbstraction, LedgerBlogDbContext>(provider => provider.GetRequiredService<LedgerBlogDbContext>());
            services.AddScoped<IUnitOfWork<ILedgerBlogDbAbstraction>, UnitOfWork<ILedgerBlogDbAbstraction>>();

            return services;
        }
    }
}
