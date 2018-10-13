using Ledger.Blog.Domain.Aggregates.CategoryAggregate;
using System;
using System.Linq;

namespace Ledger.Blog.Domain.Repositories.ArticleCategoryRepositories
{
    public interface IArticleCategoryRepository
    {
        IQueryable<ArticleCategory> GetAllCategories();
        ArticleCategory GetById(Guid id);
        void Register(ArticleCategory articleCategory);
        void Update(ArticleCategory articleCategory);
        void Remove(ArticleCategory articleCategory);
    }
}
