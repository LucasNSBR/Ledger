using Ledger.Shared.Entities;

namespace Ledger.Activations.Domain.Aggregates.ActivationAggregate
{
    public class Company : Entity<Company>
    {
        public Owner Owner { get; private set; }

        public byte[] ContratoSocialPicture { get; private set; }
        public byte[] AlteracaoContratoSocialPicture { get; private set; }

        public Company(Owner owner, byte[] contratoSocial, byte[] alteracaoContratoSocial)
        {
            Owner = owner;
            ContratoSocialPicture = contratoSocial;
            AlteracaoContratoSocialPicture = alteracaoContratoSocial;
        }
    }
}
