using Ledger.Blog.Data.EntityTypeConfiguration;
using Ledger.Blog.Data.EntityTypeConfiguration.ArticleCategoryTypeConfiguration;
using Ledger.Blog.Data.EntityTypeConfiguration.ArticleTypeConfiguration;
using Ledger.Blog.Domain.Aggregates.ArticleAggregate;
using Ledger.Blog.Domain.Aggregates.CategoryAggregate;
using Microsoft.EntityFrameworkCore;

namespace Ledger.Blog.Data.Context
{
    public class LedgerBlogDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }

        public LedgerBlogDbContext(DbContextOptions<LedgerBlogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleCategoryEntityTypeConfiguration());
        }
    }
}
