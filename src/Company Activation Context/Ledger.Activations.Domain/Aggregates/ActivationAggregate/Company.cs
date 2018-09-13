using Ledger.Shared.Entities;
using System;

namespace Ledger.Activations.Domain.Aggregates.ActivationAggregate
{
    public class Company : Entity<Company>
    {
        //Company Social Contract (similar to a Operating Agreement Contract)
        public byte[] ContratoSocialPicture { get; private set; }

        //Updated Company Social Contract (similar to Operating Agreement Contract with last changes)
        public byte[] AlteracaoContratoSocialPicture { get; private set; }

        //Owner Document (CPF, RG or Driver license)
        public byte[] OwnerDocumentPicture { get; private set; }

        //Extra Document (Can be anything)
        public byte[] ExtraDocumentPicture { get; private set; }

        protected Company() { }

        public Company(Guid id)
        {
            Id = id;
        }

        public void AttachCompanyDocuments(byte[] contratoSocial, byte[] alteracaoContratoSocial, byte[] ownerDocument, byte[] extraDocument)
        {
            ContratoSocialPicture = contratoSocial;
            AlteracaoContratoSocialPicture = alteracaoContratoSocial;
            OwnerDocumentPicture = ownerDocument;
            ExtraDocumentPicture = extraDocument;
        }
    }
}
