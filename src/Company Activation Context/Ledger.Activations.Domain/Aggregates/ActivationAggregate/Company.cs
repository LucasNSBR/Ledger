using Ledger.Shared.Entities;
using Ledger.Shared.ValueObjects;
using System;

namespace Ledger.Activations.Domain.Aggregates.ActivationAggregate
{
    public class Company : Entity<Company>
    {
        //Company Social Contract (similar to a Operating Agreement Contract)
        public Image ContratoSocialPicture { get; private set; }

        //Updated Company Social Contract (similar to Operating Agreement Contract with last changes)
        public Image AlteracaoContratoSocialPicture { get; private set; }

        //Owner Document (CPF, RG or Driver license)
        public Image OwnerDocumentPicture { get; private set; }

        //Extra Document (Can be anything)
        public Image ExtraDocumentPicture { get; private set; }

        protected Company() { }

        public Company(Guid id)
        {
            Id = id;
        }

        public void AttachCompanyDocuments(Image contratoSocial, Image alteracaoContratoSocial, Image ownerDocument, Image extraDocument)
        {
            ContratoSocialPicture = contratoSocial;
            AlteracaoContratoSocialPicture = alteracaoContratoSocial;
            OwnerDocumentPicture = ownerDocument;
            ExtraDocumentPicture = extraDocument;
        }
    }
}
