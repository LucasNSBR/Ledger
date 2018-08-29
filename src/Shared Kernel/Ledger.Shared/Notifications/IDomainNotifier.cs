using System.Collections.Generic;

namespace Ledger.Shared.Notifications
{
    public interface IDomainNotifier
    {
        IReadOnlyList<DomainNotification> GetNotifications();
        void AddNotification(string title, string description);
        void AddNotification(DomainNotification notification);
        bool HasNotifications();
    }
}
