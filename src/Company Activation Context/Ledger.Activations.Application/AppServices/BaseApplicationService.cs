﻿using Ledger.CrossCutting.Data.Transactions;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.IntegrationEvents.Events;
using Ledger.Shared.Notifications;

namespace Ledger.Activations.Application.AppServices
{
    public abstract class BaseApplicationService
    {
        private readonly IDomainNotificationHandler _domainNotificationHandler;
        private readonly IServiceBus _serviceBus;
        private readonly IUnitOfWork _unitOfWork;

        public BaseApplicationService(IDomainNotificationHandler domainNotificationHandler, IUnitOfWork unitOfWork, IServiceBus serviceBus)
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

        public void Publish(IntegrationEvent integrationEvent)
        {
            _serviceBus.Publish(integrationEvent);
        }
    }
}
