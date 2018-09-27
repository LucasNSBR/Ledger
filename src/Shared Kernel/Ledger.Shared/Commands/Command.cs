using Ledger.Shared.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.Shared.Commands
{
    public abstract class Command : ICommand, IDomainNotifier
    {
        public Guid CommandId { get; }
        public DateTime DateCreated { get; }

        private readonly List<DomainNotification> _notifications;

        protected Command()
        {
            CommandId = Guid.NewGuid();
            DateCreated = DateTime.Now;

            _notifications = new List<DomainNotification>();
        }

        public abstract void Validate();

        public IReadOnlyList<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public void AddNotification(string title, string description)
        {
            _notifications.Add(new DomainNotification(title, description));
        }

        public void AddNotification(DomainNotification notification)
        {
            if (notification == null)
                return;

            _notifications.Add(notification);
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }
    }
}
