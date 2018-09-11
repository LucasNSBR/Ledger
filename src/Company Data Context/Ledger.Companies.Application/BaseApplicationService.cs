using Ledger.Companies.Data.Context;
using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.Notifications;

namespace Ledger.Companies.Application.AppServices
{
    public abstract class BaseApplicationService
    {
        private readonly IDomainNotificationHandler _domainNotificationHandler;
        protected readonly IServiceBus _serviceBus;
        private readonly IUnitOfWork<ILedgerCompanyDbAbstraction> _unitOfWork;

        public BaseApplicationService(IDomainNotificationHandler domainNotificationHandler, IUnitOfWork<ILedgerCompanyDbAbstraction> unitOfWork, IServiceBus serviceBus)
        {
            _domainNotificationHandler = domainNotificationHandler;
            _unitOfWork = unitOfWork;
            _serviceBus = serviceBus;
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
    }
}
