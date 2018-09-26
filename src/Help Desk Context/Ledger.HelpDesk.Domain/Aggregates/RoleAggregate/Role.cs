using Ledger.Shared.Entities;

namespace Ledger.HelpDesk.Domain.Aggregates.RoleAggregate
{
    public class Role : Entity<Role>
    {
        public string Name { get; private set; }

        public Role(string name)
        {
            Name = name;
        }
    }
}
