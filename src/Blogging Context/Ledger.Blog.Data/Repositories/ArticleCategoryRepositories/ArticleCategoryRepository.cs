using Ledger.Blog.Data.Context;
using Ledger.Blog.Domain.Aggregates.CategoryAggregate;
using Ledger.Blog.Domain.Repositories.ArticleCategoryRepositories;
using Ledger.Blog.Domain.Specifications.ArticleCategorySpecifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Ledger.Blog.Data.Repositories.ArticleCategoryRepositories
{
    public class ArticleCategoryRepository : IArticleCategoryRepository
    {
        private readonly LedgerBlogDbContext _dbContext;
        private readonly DbSet<ArticleCategory> _dbSet;

        public ArticleCategoryRepository(LedgerBlogDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.ArticleCategories;
        }

        public IQueryable<ArticleCategory> GetAllCategories()
        {
            return _dbSet
                .AsNoTracking();
        }

        public ArticleCategory GetById(Guid id)
        {
            ArticleCategoryIdSpecification specification = new ArticleCategoryIdSpecification(id);

            return _dbSet
                .AsNoTracking()
                .FirstOrDefault(specification.ToExpression());
        }

        public void Register(ArticleCategory articleCategory)
        {
            _dbContext.Add(articleCategory);
        }

        public void Update(ArticleCategory articleCategory)
        {
            _dbContext.Update(articleCategory);
        }

        public void Remove(ArticleCategory articleCategory)
        {
            _dbContext.Remove(articleCategory);
        }
    }
}
