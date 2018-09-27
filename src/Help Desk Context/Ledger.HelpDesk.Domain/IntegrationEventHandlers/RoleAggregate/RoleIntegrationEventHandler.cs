using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.HelpDesk.Domain.Aggregates.RoleAggregate;
using Ledger.HelpDesk.Domain.Context;
using Ledger.HelpDesk.Domain.Repositories.RoleRepositories;
using Ledger.Shared.IntegrationEvents.Events.RoleEvents;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Ledger.HelpDesk.Domain.IntegrationEventHandlers.RoleAggregate
{
    public class RoleIntegrationEventHandler : IConsumer<RoleRegisteredIntegrationEvent>,
                                               IConsumer<RoleRemovedIntegrationEvent>
    {
        private readonly IRoleRepository _repository;
        private readonly IUnitOfWork<ILedgerHelpDeskDbAbstraction> _unitOfWork;
        
        public RoleIntegrationEventHandler(IRoleRepository repository, IUnitOfWork<ILedgerHelpDeskDbAbstraction> unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Task Consume(ConsumeContext<RoleRegisteredIntegrationEvent> context)
        {
            Guid id = context.Message.RoleId;
            string name = context.Message.Name;

            Role role = new Role(id, name);

            _repository.Register(role);
            _unitOfWork.Commit();

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<RoleRemovedIntegrationEvent> context)
        {
            string name = context.Message.RoleName;

            Role role = _repository.GetByName(name);

            if (role == null)
                throw new InvalidOperationException("Role not found");

            _repository.Remove(role);
            _unitOfWork.Commit();
            
            return Task.CompletedTask;
        }
    }
}
