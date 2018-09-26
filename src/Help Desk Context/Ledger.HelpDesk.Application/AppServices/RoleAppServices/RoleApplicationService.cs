using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.HelpDesk.Domain.Aggregates.RoleAggregate;
using Ledger.HelpDesk.Domain.Commands.RoleCommands;
using Ledger.HelpDesk.Domain.Context;
using Ledger.HelpDesk.Domain.Repositories.RoleRepositories;
using Ledger.Shared.Notifications;
using System;

namespace Ledger.HelpDesk.Application.AppServices.RoleAppServices
{
    public class RoleApplicationService : BaseApplicationService, IRoleApplicationService
    {
        private readonly IRoleRepository _repository;

        public RoleApplicationService(IRoleRepository repository, IDomainNotificationHandler domainNotificationHandler, IUnitOfWork<ILedgerHelpDeskDbAbstraction> unitOfWork, IDomainServiceBus domainBus) : base(domainNotificationHandler, unitOfWork, domainBus)
        {
        }

        public Role GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public Role GetByName(string name)
        {
            return _repository.GetByName(name);
        }

        public void Register(RegisterRoleCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Role role = new Role(command.Name);

            if (NotifyRoleExists(role.Name))
                return;

            _repository.Register(role);

            Commit();
        }

        public void Remove(RemoveRoleCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Role role = _repository.GetById(command.RoleId);

            if (NotifyNullRole(role))
                return;

            _repository.Remove(role);

            Commit();
        }

        private bool NotifyRoleExists(string name)
        {
            bool exists = _repository.RoleExists(name);

            if (exists)
            {
                AddNotification("Role duplicado", "Uma role com o mesmo valor já foi registrada.");
                return true;
            }

            return false;
        }

        private bool NotifyNullRole(Role role)
        {
            if (role == null)
            {
                AddNotification("Id inválido", "A role não pôde ser encontrada.");
                return true;
            }

            return false;
        }
    }
}
