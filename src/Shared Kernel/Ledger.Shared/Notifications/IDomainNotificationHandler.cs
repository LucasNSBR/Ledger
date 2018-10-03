using System.Collections.Generic;

namespace Ledger.Shared.Notifications
{
    public interface IDomainNotificationHandler
    {
        bool HasNotifications();
        void AddNotification(DomainNotification domainNotification);
        void AddNotification(string title, string description);
        bool AddNotifications(IDomainNotifier notifier);
        bool AddNotifications(List<DomainNotification> notifications);
        IReadOnlyList<DomainNotification> GetNotifications();
        IEnumerable<DomainNotification> GetAtomicNotifications();
        string GetNotificationsAsString();
    }
}
