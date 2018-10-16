using Ledger.Blog.Domain.Aggregates.CategoryAggregate;
using Ledger.Blog.Domain.Repositories.ArticleCategoryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.Blog.Tests.Mocks
{
    public class FakeArticleCategoryRepository : IArticleCategoryRepository
    {
        private readonly List<ArticleCategory> _categories;

        public FakeArticleCategoryRepository()
        {
            _categories = new List<ArticleCategory>();
        }

        public IQueryable<ArticleCategory> GetAllCategories()
        {
            return _categories.AsQueryable();
        }

        public ArticleCategory GetById(Guid id)
        {
            return _categories.FirstOrDefault(c => c.Id == id);
        }

        public void Register(ArticleCategory articleCategory)
        {
            _categories.Add(articleCategory);
        }

        public void Remove(ArticleCategory articleCategory)
        {
            _categories.Remove(articleCategory);
        }

        public void Update(ArticleCategory articleCategory)
        {
            int index = _categories.FindIndex(a => a.Id == articleCategory.Id);

            _categories[index] = articleCategory;
        }
    }
}
