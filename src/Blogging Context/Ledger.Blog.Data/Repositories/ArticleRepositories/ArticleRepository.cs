using Ledger.Blog.Data.Context;
using Ledger.Blog.Domain.Aggregates.ArticleAggregate;
using Ledger.Blog.Domain.Repositories.ArticleRepositories;
using Ledger.Blog.Domain.Specifications.ArticleSpecifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Ledger.Blog.Data.Repositories.ArticleRepositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly LedgerBlogDbContext _dbContext;
        private readonly DbSet<Article> _dbSet;

        public ArticleRepository(LedgerBlogDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Articles;
        }

        public IQueryable<Article> GetAllArticles()
        {
            return _dbSet
                .AsNoTracking()
                .Include(a => a.Comments);
        }

        public IQueryable<Article> GetByCategory(Guid categoryId)
        {
            ArticleCategoryIdSpecification specification = new ArticleCategoryIdSpecification(categoryId);

            return _dbSet
                .AsNoTracking()
                .Include(a => a.Comments)
                .Where(specification.ToExpression());
        }

        public Article GetById(Guid id)
        {
            ArticleIdSpecification specification = new ArticleIdSpecification(id);

            return _dbSet
                .AsNoTracking()
                .Include(a => a.Comments)
                .FirstOrDefault(specification.ToExpression());
        }

        public void Register(Article article)
        {
            _dbContext.Add(article);
        }

        public void Update(Article article)
        {
            _dbContext.Update(article);
        }

        public void Remove(Article article)
        {
            _dbContext.Remove(article);
        }
    }
}
