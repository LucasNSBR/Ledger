using System.Collections.Generic;
using System.Linq;

namespace Ledger.Shared.Notifications
{
    public class DomainNotificationHandler : IDomainNotificationHandler
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }

        public void AddNotification(DomainNotification notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotifications(IDomainNotifier notifier)
        {
            foreach (DomainNotification notification in notifier.GetNotifications())
            {
                AddNotification(notification.Title, notification.Description);
            }
        }

        public void AddNotification(string title, string description)
        {
            _notifications.Add(new DomainNotification(title, description));
        }

        public IReadOnlyList<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public IEnumerable<DomainNotification> GetAtomicNotifications()
        {
            foreach (DomainNotification notification in _notifications)
            {
                yield return notification;
            }
        }

        public void Dispose()
        {
            _notifications.Clear();
        }
    }
}
