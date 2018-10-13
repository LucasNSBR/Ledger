using Ledger.Blog.Domain.Aggregates.ArticleAggregate;
using System;
using System.Linq;

namespace Ledger.Blog.Domain.Repositories.ArticleRepositories
{
    public interface IArticleRepository
    {
        IQueryable<Article> GetAllArticles();
        IQueryable<Article> GetByCategory(Guid categoryId);
        Article GetById(Guid id);
        void Register(Article article);
        void Update(Article article);
        void Remove(Article article);
    }
}
