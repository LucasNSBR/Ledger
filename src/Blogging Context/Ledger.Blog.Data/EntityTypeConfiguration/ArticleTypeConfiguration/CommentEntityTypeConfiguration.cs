using Ledger.Blog.Domain.Aggregates.ArticleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ledger.Blog.Data.EntityTypeConfiguration.ArticleTypeConfiguration
{
    public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(c => c.Body)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(c => c.AuthorId)
                .IsRequired();
        }
    }
}
