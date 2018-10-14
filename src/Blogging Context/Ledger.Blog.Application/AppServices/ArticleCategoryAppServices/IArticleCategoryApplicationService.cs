using Ledger.Blog.Domain.Aggregates.CategoryAggregate;
using Ledger.Blog.Domain.Commands.ArticleCategoryCommands;
using System;
using System.Linq;

namespace Ledger.Blog.Application.AppServices.ArticleCategoryAppServices
{
    public interface IArticleCategoryApplicationService
    {
        IQueryable<ArticleCategory> GetAllCategories();
        ArticleCategory GetById(Guid id);
        void Register(RegisterArticleCategoryCommand command);
        void Update(UpdateArticleCategoryCommand command);
        void Remove(RemoveArticleCategoryCommand command);
    }
}
