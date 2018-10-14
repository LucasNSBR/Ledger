using Ledger.Blog.Domain.Aggregates.CategoryAggregate;
using Ledger.Blog.Domain.Commands.ArticleCategoryCommands;
using Ledger.Blog.Domain.Context;
using Ledger.Blog.Domain.Repositories.ArticleCategoryRepositories;
using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.Notifications;
using System;
using System.Linq;

namespace Ledger.Blog.Application.AppServices.ArticleCategoryAppServices
{
    public class ArticleCategoryApplicationService : BaseApplicationService, IArticleCategoryApplicationService
    {
        private readonly IArticleCategoryRepository _repository;

        public ArticleCategoryApplicationService(IArticleCategoryRepository repository, IDomainNotificationHandler domainNotificationHandler, IUnitOfWork<ILedgerBlogDbAbstraction> unitOfWork, IIntegrationServiceBus integrationBus, IDomainServiceBus domainBus) : base(domainNotificationHandler, unitOfWork, integrationBus, domainBus)
        {
            _repository = repository;
        }

        public IQueryable<ArticleCategory> GetAllCategories()
        {
            return _repository.GetAllCategories();
        }

        public ArticleCategory GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void Register(RegisterArticleCategoryCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            ArticleCategory category = new ArticleCategory(command.Name);

            _repository.Register(category);

            Commit();
        }

        public void Update(UpdateArticleCategoryCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            ArticleCategory category = _repository.GetById(command.CategoryId);

            if (NotifyNullCategory(category))
                return;

            category = new ArticleCategory(command.Name);

            _repository.Update(category);

            Commit();
        }

        public void Remove(RemoveArticleCategoryCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            ArticleCategory category = _repository.GetById(command.CategoryId);

            if (NotifyNullCategory(category))
                return;

            _repository.Remove(category);

            Commit();
        }
    }
}
