using Ledger.Blog.Domain.Aggregates.ArticleAggregate;
using Ledger.Blog.Domain.Aggregates.CategoryAggregate;
using Ledger.Blog.Domain.Context;
using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.Events;
using Ledger.Shared.IntegrationEvents.Events;
using Ledger.Shared.Notifications;

namespace Ledger.Blog.Application.AppServices
{
    public abstract class BaseApplicationService
    {
        private readonly IDomainNotificationHandler _domainNotificationHandler;
        private readonly IIntegrationServiceBus _integrationBus;
        private readonly IDomainServiceBus _domainBus;
        private readonly IUnitOfWork<ILedgerBlogDbAbstraction> _unitOfWork;

        protected BaseApplicationService(IDomainNotificationHandler domainNotificationHandler, IUnitOfWork<ILedgerBlogDbAbstraction> unitOfWork, IIntegrationServiceBus integrationBus, IDomainServiceBus domainBus)
        {
            _domainNotificationHandler = domainNotificationHandler;
            _unitOfWork = unitOfWork;
            _integrationBus = integrationBus;
            _domainBus = domainBus;
        }

        public bool AddNotifications(IDomainNotifier notifier)
        {
            if (notifier.HasNotifications())
            {
                _domainNotificationHandler.AddNotifications(notifier);
                return true;
            }

            return false;
        }

        public void AddNotification(string title, string description)
        {
            _domainNotificationHandler.AddNotification(title, description);
        }

        public bool Commit()
        {
            return _unitOfWork.Commit().Success;
        }

        public void Publish<TIntegrationEvent>(TIntegrationEvent integrationEvent) where TIntegrationEvent : IntegrationEvent
        {
            _integrationBus.Publish(integrationEvent);
        }

        public void PublishLocal<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent: DomainEvent
        {
            _domainBus.Publish(domainEvent);
        }

        protected bool NotifyNullArticle(Article article)
        {
            if (article == null)
            {
                AddNotification("Id inválido", "O artigo não pôde ser encontrado.");
                return true;
            }

            return false;
        }

        protected bool NotifyNullCategory(ArticleCategory category)
        {
            if (category == null)
            {
                AddNotification("Id inválido", "A categoria de artigo não pôde ser encontrada.");
                return true;
            }

            return false;
        }
    }
}
