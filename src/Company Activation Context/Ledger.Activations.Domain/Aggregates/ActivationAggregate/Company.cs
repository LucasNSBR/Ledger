using Ledger.Shared.Entities;
using System;

namespace Ledger.Activations.Domain.Aggregates.ActivationAggregate
{
    public class Company : Entity<Company>
    {
        public Owner Owner { get; private set; }
        public byte[] ContratoSocialPicture { get; private set; }
        public byte[] AlteracaoContratoSocialPicture { get; private set; }
        public byte[] OwnerDocumentPicture { get; private set; }

        protected Company() { }

        public Company(Guid id, Owner owner)
        {
            Id = id;
            Owner = owner;
        }

        public void AttachCompanyDocuments(byte[] contratoSocial, byte[] alteracaoContratoSocial, byte[] ownerDocument)
        {
            ContratoSocialPicture = contratoSocial;
            AlteracaoContratoSocialPicture = alteracaoContratoSocial;
            OwnerDocumentPicture = ownerDocument;
        }
    }
}
