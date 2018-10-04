using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ledger.Shared.Notifications
{
    public class DomainNotificationHandler : IDomainNotificationHandler
    {
        private readonly List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }

        public void AddNotification(DomainNotification domainNotification)
        {
            _notifications.Add(domainNotification);
        }

        public bool AddNotifications(IDomainNotifier notifier)
        {
            if (notifier.HasNotifications())
            {
                foreach (DomainNotification notification in notifier.GetNotifications())
                {
                    AddNotification(notification);
                }

                return true;
            }

            return false;
        }

        public bool AddNotifications(List<DomainNotification> notifications)
        {
            if (notifications.Any())
            {
                foreach (DomainNotification notification in notifications)
                {
                    AddNotification(notification);
                }

                return true;
            }

            return false;
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

        public string GetNotificationsAsString()
        {
            StringBuilder builder = new StringBuilder();

            if (_notifications.Any())
            {
                foreach (DomainNotification notification in _notifications)
                {
                    builder.AppendLine($"title: {notification.Title}, message: {notification.Description} at date: {notification.DateCreated}");
                }

                return builder.ToString();
            }

            return string.Empty;
        }

        public void Clear()
        {
            _notifications.Clear();
        }
    }
}
