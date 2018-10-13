using Ledger.Blog.Domain.Aggregates.CategoryAggregate;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.Blog.Domain.Specifications.ArticleCategorySpecifications
{
    public class ArticleCategoryIdSpecification : BaseSpecification<ArticleCategory>
    {
        private readonly Guid _id;

        public ArticleCategoryIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<ArticleCategory, bool>> ToExpression()
        {
            return ac => ac.Id == _id;
        }
    }
}
