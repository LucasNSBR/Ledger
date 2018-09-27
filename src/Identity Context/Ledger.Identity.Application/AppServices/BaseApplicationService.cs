using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.Events;
using Ledger.Shared.IntegrationEvents.Events;
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
        private readonly IIntegrationServiceBus _integrationBus;

        protected BaseApplicationService(IDomainNotificationHandler domainNotificationHandler, IDomainServiceBus domainServiceBus, IIntegrationServiceBus integrationBus)
        {
            _domainNotificationHandler = domainNotificationHandler;
            _domainServiceBus = domainServiceBus;
            _integrationBus = integrationBus;
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

        public async Task Publish<TIntegrationEvent>(TIntegrationEvent integrationEvent) where TIntegrationEvent : IntegrationEvent
        {
            await _integrationBus.Publish(integrationEvent);
        }
    }
}
