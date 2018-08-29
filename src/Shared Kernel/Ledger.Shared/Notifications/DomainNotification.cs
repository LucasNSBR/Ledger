using System;

namespace Ledger.Shared.Notifications
{
    public class DomainNotification 
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime DateCreated { get; private set; }

        public DomainNotification(string title, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;

            DateCreated = DateTime.Now;
        }
    }
}
