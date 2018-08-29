using System;
using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Repositories.ActivationRepository;

namespace Ledger.Activations.Application.AppServices
{
    public class ActivationApplicationService : IActivationApplicationService
    {
        private readonly IActivationRepository _repository;

        public ActivationApplicationService(IActivationRepository repository)
        {
            _repository = repository;
        }

        public Activation GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public Activation GetByCompanyId(Guid id)
        {
            return _repository.GetByCompanyId(id);
        }
    }
}
