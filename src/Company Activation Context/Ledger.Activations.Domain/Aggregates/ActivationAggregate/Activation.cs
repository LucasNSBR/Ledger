using Ledger.Activations.Domain.Events;
using Ledger.Shared.Entities;
using System;

namespace Ledger.Activations.Domain.Aggregates.ActivationAggregate
{
    public class Activation : Entity<Activation>, IAggregateRoot
    {
        public Guid CompanyId { get; private set; }
        public Company Company { get; private set; }

        public ActivationStatus Status { get; private set; }

        public Activation(Company company)
        {
            Company = company;

            SetPending();
        }

        public Activation(Guid id, Company company)
        {
            Id = id;
            Company = company;

            SetPending();
        }

        private void SetPending()
        {
            Status = ActivationStatus.Pending;
        }

        public void SetAccepted()
        {
            if (IsPending())
            {
                Status = ActivationStatus.Accepted;
                AddEvent(new CompanyActivationAcceptedEvent());
            }
            else
                AddNotification("Erro de aceitação", "Não é possível validar a ativação a partir do status atual.");
        }

        public void SetRejected()
        {
            if (IsPending())
            {
                Status = ActivationStatus.Rejected;
                AddEvent(new CompanyActivationRejectedEvent());
            }
            else
                AddNotification("Erro de rejeição", "Não é possível rejeitar a ativação a partir do status atual.");
        }

        private bool IsPending()
        {
            return Status == ActivationStatus.Pending;
        }

        public void ResetActivationProcess()
        {
            if (Status == ActivationStatus.Rejected)
                SetPending();
            else
                AddNotification("Erro de reinício", "Não é possível recomeçar o processo a partir do status atual.");
        }
    }
}
