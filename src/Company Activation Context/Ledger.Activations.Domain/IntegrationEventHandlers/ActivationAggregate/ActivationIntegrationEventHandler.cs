using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Context;
using Ledger.Activations.Domain.Factories.ActivationFactories;
using Ledger.Activations.Domain.Repositories.ActivationRepository;
using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.Shared.IntegrationEvents.Events.CompanyEvents;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Ledger.Activations.Domain.IntegrationEventHandlers.ActivationAggregate
{
    public class ActivationIntegrationEventHandler : IConsumer<RegisteredCompanyIntegrationEvent>
    {
        private readonly IActivationRepository _repository;
        private readonly IUnitOfWork<ILedgerActivationDbAbstraction> _unitOfWork;
        private readonly IActivationFactory _factory;

        public ActivationIntegrationEventHandler(IUnitOfWork<ILedgerActivationDbAbstraction> unitOfWork, IActivationRepository repository, IActivationFactory factory)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _factory = factory;
        }

        public Task Consume(ConsumeContext<RegisteredCompanyIntegrationEvent> context)
        {
            Guid companyId = context.Message.CompanyId;
            Activation activation = _factory.CreateActivation(companyId);

            _repository.Register(activation);
            _unitOfWork.Commit();

            //TODO: Add Email Service here

            return Task.CompletedTask;
        }
    }
}
