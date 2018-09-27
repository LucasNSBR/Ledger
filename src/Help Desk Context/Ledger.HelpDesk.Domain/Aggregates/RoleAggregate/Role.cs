using Ledger.Shared.Entities;
using System;

namespace Ledger.HelpDesk.Domain.Aggregates.RoleAggregate
{
    public class Role : Entity<Role>
    {
        public string Name { get; private set; }

        public Role(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
