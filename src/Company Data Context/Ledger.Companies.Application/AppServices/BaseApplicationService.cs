using Ledger.Companies.Domain.Context;
using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.IntegrationEvents.Events;
using Ledger.Shared.Notifications;
using System.Collections.Generic;

namespace Ledger.Companies.Application.AppServices
{
    public abstract class BaseApplicationService
    {
        private readonly IDomainNotificationHandler _domainNotificationHandler;
        private readonly IIntegrationServiceBus _integrationBus;
        private readonly IUnitOfWork<ILedgerCompanyDbAbstraction> _unitOfWork;

        protected BaseApplicationService(IDomainNotificationHandler domainNotificationHandler, IUnitOfWork<ILedgerCompanyDbAbstraction> unitOfWork, IIntegrationServiceBus integrationBus)
        {
            _domainNotificationHandler = domainNotificationHandler;
            _unitOfWork = unitOfWork;
            _integrationBus = integrationBus;
        }
        
        public bool AddNotifications(IDomainNotifier notifier)
        {
            return _domainNotificationHandler.AddNotifications(notifier);
        }

        public bool AddNotifications(List<DomainNotification> notifications)
        {
            return _domainNotificationHandler.AddNotifications(notifications);
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
    }
}
