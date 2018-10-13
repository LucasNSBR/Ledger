using Ledger.Blog.Domain.Aggregates.ArticleAggregate;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.Blog.Domain.Specifications.ArticleSpecifications
{
    public class ArticleIdSpecification : BaseSpecification<Article>
    {
        private readonly Guid _id;

        public ArticleIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<Article, bool>> ToExpression()
        {
            return a => a.Id == _id;
        }
    }
}
