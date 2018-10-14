using Ledger.Blog.Domain.Aggregates.ArticleAggregate;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.Blog.Domain.Specifications.ArticleSpecifications
{
    public class ArticleCategoryIdSpecification : BaseSpecification<Article>
    {
        private readonly Guid _id;

        public ArticleCategoryIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<Article, bool>> ToExpression()
        {
            return ac => ac.CategoryId == _id;
        }
    }
}
