using System;

namespace Ledger.Shared.Notifications
{
    public class DomainNotification 
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }
        public DateTime DateCreated { get; }

        public DomainNotification(string title, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;

            DateCreated = DateTime.Now;
        }
    }
}
