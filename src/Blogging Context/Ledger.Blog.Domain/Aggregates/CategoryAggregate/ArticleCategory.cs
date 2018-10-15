using Ledger.Shared.Entities;
using System;

namespace Ledger.Blog.Domain.Aggregates.CategoryAggregate
{
    public class ArticleCategory : Entity<ArticleCategory>
    {
        public string Name { get; private set; }

        protected ArticleCategory() { }

        public ArticleCategory(string name)
        {
            Name = name;
        }

        public ArticleCategory(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
