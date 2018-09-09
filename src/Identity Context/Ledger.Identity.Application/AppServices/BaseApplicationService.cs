using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Ledger.Identity.Application.AppServices
{
    public abstract class BaseApplicationService
    {
        private readonly IDomainNotificationHandler _domainNotificationHandler;
        
        public BaseApplicationService(IDomainNotificationHandler domainNotificationHandler)
        {
            _domainNotificationHandler = domainNotificationHandler;
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

        public bool AddNotifications(IdentityResult result)
        {
            if (result.Errors.Any())
            {
                foreach (IdentityError error in result.Errors)
                {
                    AddNotification(error.Code, error.Description);
                }

                return true;
            }

            return false;
        }

        public void AddNotification(string title, string description)
        {
            _domainNotificationHandler.AddNotification(title, description);
        }
    }
}
