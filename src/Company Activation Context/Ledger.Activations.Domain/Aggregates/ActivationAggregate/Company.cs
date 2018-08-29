using Ledger.Shared.Entities;
using System;

namespace Ledger.Activations.Domain.Aggregates.ActivationAggregate
{
    public class Company : Entity<Company>
    {
        public Owner Owner { get; private set; }

        public byte[] ContratoSocialPicture { get; private set; }
        public byte[] AlteracaoContratoSocialPicture { get; private set; }

        protected Company() { }

        public Company(Owner owner, byte[] contratoSocial, byte[] alteracaoContratoSocial)
        {
            Owner = owner;
            ContratoSocialPicture = contratoSocial;
            AlteracaoContratoSocialPicture = alteracaoContratoSocial;
        }

        public Company(Guid id, Owner owner, byte[] contratoSocial, byte[] alteracaoContratoSocial)
        {
            Id = id;
            Owner = owner;
            ContratoSocialPicture = contratoSocial;
            AlteracaoContratoSocialPicture = alteracaoContratoSocial;
        }
    }
}
