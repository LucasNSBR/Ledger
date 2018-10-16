using Ledger.Blog.Domain.Aggregates.ArticleAggregate;
using Ledger.Blog.Domain.Repositories.ArticleRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.Blog.Tests.Mocks
{
    public class FakeArticleRepository : IArticleRepository
    {
        private readonly List<Article> _articles;

        public FakeArticleRepository()
        {
            _articles = new List<Article>();
        }

        public IQueryable<Article> GetAllArticles()
        {
            return _articles.AsQueryable();
        }

        public IQueryable<Article> GetByCategory(Guid categoryId)
        {
            return _articles.Where(a => a.CategoryId == categoryId).AsQueryable();
        }

        public Article GetById(Guid id)
        {
            return _articles.FirstOrDefault(a => a.Id == id);
        }

        public void Register(Article article)
        {
            _articles.Add(article);
        }

        public void Remove(Article article)
        {
            _articles.Remove(article);
        }

        public void Update(Article article)
        {
            int index = _articles.FindIndex(a => a.Id == article.Id);

            _articles[index] = article;
        }
    }
}
