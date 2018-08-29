using System;
using Ledger.Activations.Domain.Aggregates.ActivationAggregate;

namespace Ledger.Activations.Application.AppServices
{
    public class ActivationApplicationService : IActivationApplicationService
    {
        public Activation GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Activation GetByCompanyId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
