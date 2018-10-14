using Ledger.Blog.Domain.Aggregates.ArticleAggregate;
using Ledger.Blog.Domain.Commands.ArticleCommands;
using System;
using System.Linq;

namespace Ledger.Blog.Application.AppServices.ArticleAppServices
{
    public interface IArticleApplicationService
    {
        IQueryable<Article> GetAllArticles();
        IQueryable<Article> GetByCategory(Guid categoryId);
        Article GetById(Guid id);
        void Register(RegisterArticleCommand command);
        void Update(UpdateArticleCommand command);
        void SetActive(SetActiveArticleCommand command);
        void SetInactive(SetInactiveArticleCommand command);
        void AddComment(AddArticleCommentCommand command);
        void RemoveComment(RemoveArticleCommentCommand command);

        void Remove(RemoveArticleCommand command);
    }
}
