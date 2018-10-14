using Ledger.Shared.Entities;
using System;

namespace Ledger.Blog.Domain.Aggregates.ArticleAggregate
{
    public class Comment : Entity<Comment>
    {
        public Guid ArticleId { get; private set; }
        public Guid AuthorId { get; private set; }
        public string Body { get; private set; }

        public Comment(Guid articleId, Guid authorId, string body)
        {
            ArticleId = articleId;
            AuthorId = authorId;
            Body = body;
        }

        public Comment(Guid id, Guid articleId, Guid authorId, string body)
        {
            Id = id;
            ArticleId = articleId;
            AuthorId = authorId;
            Body = body;
        }
    }
}
