using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Commands;
using System;

namespace Ledger.Activations.Application.AppServices.ActivationAppServices
{
    public interface IActivationApplicationService
    {
        Activation GetById(Guid id);
        
        void AttachCompanyDocuments(AttachCompanyDocumentsCommand command);
        void AcceptActivation(AcceptActivationCommand command);
        void RejectActivation(RejectActivationCommand command);
        void ResetActivation(ResetActivationCommand command);
    }
}
