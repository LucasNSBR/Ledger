using Ledger.Shared.Entities;
using Ledger.Shared.ValueObjects;
using System.Collections.Generic;

namespace Ledger.Activations.Domain.Aggregates.ActivationAggregate
{
    public class Activation : Entity<Activation>, IAggregateRoot
    {
        public Company Company { get; private set; }

        public ActivationStatus Status { get; private set; }

        protected Activation() { }

        //The company can only have one activation, 
        //then ActivationId is the same as CompanyId
        public Activation(Company company)
        {
            Id = company.Id;
            Company = company;

            SetPending();
        }

        private void SetPending()
        {
            if (!IsPending())
                Status = ActivationStatus.Pending;
        }

        public void SetAccepted()
        {
            if (IsPending())
                Status = ActivationStatus.Accepted;
            else
                AddNotification("Erro de aceitação", "Não é possível validar a ativação a partir do status atual.");
        }

        public void SetRejected()
        {
            if (IsPending())
                Status = ActivationStatus.Rejected;
            else
                AddNotification("Erro de rejeição", "Não é possível rejeitar a ativação a partir do status atual.");
        }

        public bool IsPending()
        {
            return Status == ActivationStatus.Pending;
        }

        public bool IsAccepted()
        {
            return Status == ActivationStatus.Accepted;
        }

        public void ResetActivationProcess()
        {
            if (Status == ActivationStatus.Rejected)
                SetPending();
            else
                AddNotification("Erro de reinício", "Não é possível recomeçar o processo a partir do status atual.");
        }

        public void AttachCompanyDocuments(Image contratoSocial, Image alteracaoContratoSocial, Image ownerDocument, Image extraDocument)
        {
            if (Company == null)
                AddNotification("Erro na entidade", "Não há uma empresa válida presente no processo de ativação.");
            else
            {
                if (IsPending())
                    Company.AttachCompanyDocuments(contratoSocial, alteracaoContratoSocial, ownerDocument, extraDocument);
                else
                    AddNotification("Erro de anexação", "Não é possível anexar os documentos a partir do status atual.");
            }
        }

        public IReadOnlyList<Image> GetCompanyDocuments()
        {
            if (Company == null)
            {
                AddNotification("Erro na entidade", "Não há uma empresa válida presente no processo de ativação.");
                return null;
            }
            else
            {
                List<Image> documents = new List<Image>
                {
                    Company.ContratoSocialPicture,
                    Company.AlteracaoContratoSocialPicture,
                    Company.OwnerDocumentPicture,
                    Company.ExtraDocumentPicture
                };

                documents.RemoveAll(i => i == null);

                return documents;
            }
        }
    }
}
