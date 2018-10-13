using Ledger.Shared.Entities;

namespace Ledger.Blog.Domain.Aggregates.CategoryAggregate
{
    public class ArticleCategory : Entity<ArticleCategory>
    {
        public string Name { get; private set; }

        protected ArticleCategory(string name)
        {
            Name = name;
        }
    }
}
