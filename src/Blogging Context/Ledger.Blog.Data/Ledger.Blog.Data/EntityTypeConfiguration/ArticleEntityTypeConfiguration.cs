using Ledger.Blog.Domain.Aggregates.ArticleAggregate;
using Ledger.Blog.Domain.Aggregates.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.Blog.Data.EntityTypeConfiguration
{
    public class ArticleEntityTypeConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(a => a.Slug)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(a => a.Body)
                .IsRequired()
                .HasMaxLength(5000);

            builder
                .Property(a => a.AuthorId)
                .IsRequired();

            builder
                .HasOne<ArticleCategory>()
                .WithMany()
                .HasForeignKey(a => a.CategoryId);

            builder
                .HasMany(a => a.Comments)
                .WithOne()
                .HasForeignKey(c => c.ArticleId);
        }
    }
}
