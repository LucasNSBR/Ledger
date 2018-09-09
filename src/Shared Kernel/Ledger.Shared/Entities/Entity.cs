using Ledger.Shared.Events;
using Ledger.Shared.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.Shared.Entities
{
    public class Entity<T> : IDomainNotifier, IEventProducer
                             where T : Entity<T>
    {
        public Guid Id { get; protected set; }
        private List<DomainNotification> _notifications;
        private List<DomainEvent> _events;

        public Entity()
        {
            Id = Guid.NewGuid();

            _notifications = new List<DomainNotification>();
            _events = new List<DomainEvent>();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            Entity<T> other = obj as Entity<T>;

            if (other == null)
                return false;
            if (Id == other.Id)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * 997;
        }

        public static bool operator ==(Entity<T> a, Entity<T> b)
        {
            if(ReferenceEquals(a, null))
            {
                return ReferenceEquals(b, null);
            }

            return Equals(a, b);
        }

        public static bool operator !=(Entity<T> a, Entity<T> b)
        {
            if (ReferenceEquals(a, null))
            {
                return !ReferenceEquals(b, null);
            }

            return !Equals(a, b);
        }

        public IReadOnlyList<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public void AddNotification(DomainNotification notification)
        {
            if (notification == null)
                return;

            _notifications.Add(notification);
        }

        public void AddNotification(string title, string description)
        {
            DomainNotification notification = new DomainNotification(title, description);
            _notifications.Add(notification);
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }

        public IReadOnlyList<DomainEvent> GetEvents()
        {
            return _events;
        }

        public void AddEvent(DomainEvent @event)
        {
            _events.Add(@event);
        }

        public bool HasEvents()
        {
            return _events.Any();
        }
    }
}
