using Ledger.HelpDesk.Domain.Aggregates.RoleAggregate;
using Ledger.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.HelpDesk.Domain.Aggregates.UserAggregate
{
    public class User : Entity<User>, IAggregateRoot
    {
        public string Email { get; private set; }
        private readonly List<UserRole> _roles;
        public IReadOnlyList<UserRole> Roles
        {
            get
            {
                return _roles;
            }
        }

        protected User()
        {
            _roles = new List<UserRole>();
        }

        public User(Guid id, string email)
        {
            Id = id;
            Email = email;

            _roles = new List<UserRole>();
        }

        public bool IsInRole(Role role)
        {
            return _roles.Any(ur => ur.RoleId == role.Id);
        }

        public void AddRole(Role role)
        {
            UserRole userRole = new UserRole(Id, role.Id);

            if (!IsInRole(role))
                _roles.Add(userRole);
            else
                AddNotification("Já possui a permissão", "O usuário já possui a permissão especificada.");
        }

        public void RemoveRole(Role role)
        {
            UserRole userRole = new UserRole(Id, role.Id);

            if (IsInRole(role))
                _roles.Remove(userRole);
            else
                AddNotification("Não possui a permissão", "O usuário não possui a permissão especificada.");
        }
    }
}
