using Ledger.Shared.Entities;
using System;

namespace Ledger.Blog.Domain.Aggregates.ArticleAggregate
{
    public class Comment : Entity<Comment>
    {
        public Guid AuthorId { get; private set; }
        public string Body { get; private set; }

        public Comment(Guid authorId, string body)
        {
            AuthorId = authorId;
            Body = body;
        }

        public Comment(Guid id, Guid authorId, string body)
        {
            Id = id;
            AuthorId = authorId;
            Body = body;
        }
    }
}
