using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.Events;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ledger.Identity.Application.AppServices
{
    public abstract class BaseApplicationService
    {
        private readonly IDomainNotificationHandler _domainNotificationHandler;
        private readonly IDomainServiceBus _domainServiceBus;

        public BaseApplicationService(IDomainNotificationHandler domainNotificationHandler, IDomainServiceBus domainServiceBus)
        {
            _domainNotificationHandler = domainNotificationHandler;
            _domainServiceBus = domainServiceBus;
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

        public async Task PublishLocal<TDomainEvent>(TDomainEvent @event, CancellationToken? cancellationToken = null) where TDomainEvent: DomainEvent
        {
            await _domainServiceBus.Publish(@event, cancellationToken);
        }
    }
}
