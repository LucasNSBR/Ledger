using Ledger.CrossCutting.Data.Transactions;
using Ledger.Shared.Notifications;

namespace Ledger.Activations.Application.AppServices
{
    public abstract class BaseApplicationService
    {
        protected readonly IDomainNotificationHandler _domainNotificationHandler;
        private readonly IUnitOfWork _unitOfWork;

        public BaseApplicationService(IDomainNotificationHandler domainNotificationHandler, IUnitOfWork unitOfWork)
        {
            _domainNotificationHandler = domainNotificationHandler;
            _unitOfWork = unitOfWork;
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
            if (_unitOfWork.Commit())
                return true;

            return false;
        }
    }
}
