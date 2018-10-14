using Ledger.Blog.Domain.Aggregates.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.Blog.Data.EntityTypeConfiguration
{
    public class ArticleCategoryEntityTypeConfiguration : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(ac => ac.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
