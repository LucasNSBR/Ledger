using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.HelpDesk.Domain.Aggregates.RoleAggregate;
using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using Ledger.HelpDesk.Domain.Context;
using Ledger.HelpDesk.Domain.Repositories.RoleRepositories;
using Ledger.HelpDesk.Domain.Repositories.UserRepositories;
using Ledger.Shared.IntegrationEvents.Events.UserEvents;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Ledger.HelpDesk.Domain.IntegrationEventHandlers.UserAggregate
{
    public class UserIntegrationEventHandler : IConsumer<UserRegisteredIntegrationEvent>,
                                               IConsumer<UserAddedToRoleIntegrationEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork<ILedgerHelpDeskDbAbstraction> _unitOfWork;

        public UserIntegrationEventHandler(IUserRepository userRepository, IRoleRepository roleRepository, IUnitOfWork<ILedgerHelpDeskDbAbstraction> unitOfWork)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
        {
            Guid id = context.Message.UserId;
            string email = context.Message.Email;

            User user = new User(id, email);

            _userRepository.Register(user);
            _unitOfWork.Commit();

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<UserAddedToRoleIntegrationEvent> context)
        {
            Guid id = context.Message.UserId;
            string roleName = context.Message.RoleName;

            User user = _userRepository.GetById(id);
            Role role = _roleRepository.GetByName(roleName);

            if (user == null)
                throw new InvalidOperationException($"User {id} not found");
            if (role == null)
                throw new InvalidOperationException($"Role {roleName} not found");

            user.AddRole(role);

            _userRepository.Update(user);
            _unitOfWork.Commit();

            return Task.CompletedTask;
        }
    }
}
