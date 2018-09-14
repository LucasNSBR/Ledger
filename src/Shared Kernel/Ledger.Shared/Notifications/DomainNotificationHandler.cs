using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public bool AddNotifications(IDomainNotifier notifier)
        {
            if (notifier.HasNotifications())
            {
                foreach (DomainNotification notification in notifier.GetNotifications())
                {
                    AddNotification(notification.Title, notification.Description);
                    return true;
                }
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

        public string GetAndFormatNotifications()
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

        public void Dispose()
        {
            _notifications.Clear();
        }
    }
}
