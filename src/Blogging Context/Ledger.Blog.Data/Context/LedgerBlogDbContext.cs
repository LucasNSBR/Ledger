using Ledger.Blog.Data.EntityTypeConfiguration.ArticleCategoryTypeConfiguration;
using Ledger.Blog.Data.EntityTypeConfiguration.ArticleTypeConfiguration;
using Ledger.Blog.Domain.Aggregates.ArticleAggregate;
using Ledger.Blog.Domain.Aggregates.CategoryAggregate;
using Ledger.Blog.Domain.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.Blog.Data.Context
{
    public class LedgerBlogDbContext : DbContext, ILedgerBlogDbAbstraction
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }

        public LedgerBlogDbContext(DbContextOptions<LedgerBlogDbContext> options) : base(options)
        {
            Seed();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CommentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleCategoryEntityTypeConfiguration());
        }

        private void Seed()
        {
            ArticleCategory category;

            if(ArticleCategories.Count() == 0)
            {
                category = new ArticleCategory(new Guid("05327e40-1d6c-47bc-ac92-5e0df83482ed"), "Política e Brasil");
            }
            else
            {
                category = new ArticleCategory(Guid.NewGuid(), $"Categoria: {Guid.NewGuid()}");
            }

            Add(category);
            SaveChanges();
            List<Article> articles = new List<Article>
            {
                new Article("bolsonaro-preso", "Bolsonaro Preso", "Bolsonaro preso em Brasília", category.Id, Guid.NewGuid()),
                new Article("lula-preso", "Lula Preso", "Lula preso em Brasília", category.Id, Guid.NewGuid()),
                new Article("ciro-preso", "Ciro Preso", "Ciro Gomes preso em Brasília", category.Id, Guid.NewGuid()),
            };

            AddRange(articles);
            SaveChanges();
        }
    }
}
