using Ledger.Shared.Entities;
using System;

namespace Ledger.HelpDesk.Domain.Aggregates.RoleAggregate
{
    public class UserRole : EntityRelationship<Guid>
    {
        public Guid UserId { get; private set; }
        public Guid RoleId { get; private set; }
        public Role Role { get; set; }

        protected UserRole() { }

        public UserRole(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            UserRole other = obj as UserRole;

            if (other == null)
                return false;
            if (UserId == other.UserId && RoleId == other.RoleId)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return (UserId.GetHashCode() * 997) + (RoleId.GetHashCode() * 997);
        }
    }
}
