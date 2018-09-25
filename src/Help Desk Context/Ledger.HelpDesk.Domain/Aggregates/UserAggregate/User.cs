using Ledger.HelpDesk.Domain.Aggregates.UserAggregate.Roles;
using Ledger.HelpDesk.Domain.Specifications.UserSpecifications.RoleSpecifications;
using Ledger.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.HelpDesk.Domain.Aggregates.UserAggregate
{
    public class User : Entity<User>, IAggregateRoot
    {
        public string Email { get; private set; }
        private List<UserRole> _roles;

        public IReadOnlyList<UserRole> Roles
        {
            get
            {
                return _roles;
            }
        }

        protected User() { }

        public User(Guid id, string email)
        {
            Id = id;
            Email = email;

            _roles = new List<UserRole>();
        }

        public bool IsInRole(string name)
        {
            RoleNameSpecification specification = new RoleNameSpecification(name);
            return _roles.Any(specification.Compile());
        }

        public void AddRole(UserRole role)
        {
            if (!IsInRole(role.RoleName))
                _roles.Add(role);
            else
                AddNotification("Já possui a permissão", "O usuário já possui a permissão especificada.");
        }

        public void RemoveRole(UserRole role)
        {
            if (IsInRole(role.RoleName))
                _roles.Remove(role);
            else
                AddNotification("Não possui a permissão", "O usuário não possui a permissão especificada.");
        }
    }
}
