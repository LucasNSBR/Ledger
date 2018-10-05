using Ledger.Shared.Notifications;
using System.Collections.Generic;

namespace Ledger.CrossCutting.Storage.Service
{
    public class StorageResult
    {
        public bool Success { get; }
        public string FileUrl { get; }
        public List<DomainNotification> _notifications;
        public IReadOnlyList<DomainNotification> Notifications
        {
            get
            {
                return _notifications;
            }
        }

        private StorageResult(string fileUrl)
        {
            Success = true;
            FileUrl = fileUrl;
        }

        private StorageResult(List<DomainNotification> notifications)
        {
            Success = false;
            _notifications = notifications;
        }

        public static StorageResult Ok(string fileUrl)
        {
            return new StorageResult(fileUrl);
        }

        public static StorageResult Failure(params DomainNotification[] notifications)
        {
            List<DomainNotification> list = new List<DomainNotification>();

            foreach (DomainNotification notification in notifications)
            {
                list.Add(notification);
            }

            return new StorageResult(list);
        }
    }
}
