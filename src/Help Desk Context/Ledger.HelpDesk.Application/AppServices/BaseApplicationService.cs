using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.HelpDesk.Domain.Context;
using Ledger.Shared.Events;
using Ledger.Shared.Notifications;

namespace Ledger.HelpDesk.Application.AppServices
{
    public abstract class BaseApplicationService
    {
        private readonly IDomainNotificationHandler _domainNotificationHandler;
        private readonly IDomainServiceBus _domainBus;
        private readonly IUnitOfWork<ILedgerHelpDeskDbAbstraction> _unitOfWork;

        public BaseApplicationService(IDomainNotificationHandler domainNotificationHandler, IUnitOfWork<ILedgerHelpDeskDbAbstraction> unitOfWork, IDomainServiceBus domainBus)
        {
            _domainNotificationHandler = domainNotificationHandler;
            _unitOfWork = unitOfWork;
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
        
        public void PublishLocal<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent
        {
            _domainBus.Publish(domainEvent);
        }
    }
}
