using System.Collections.Generic;

namespace Ledger.Shared.Notifications
{
    public interface IDomainNotificationHandler
    {
        bool HasNotifications();
        void AddNotification(DomainNotification domainNotification);
        void AddNotification(string title, string description);
        void AddNotifications(IDomainNotifier notifier);
        IReadOnlyList<DomainNotification> GetNotifications();
        IEnumerable<DomainNotification> GetAtomicNotifications();
        void Dispose();
    }
}
