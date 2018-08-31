using Ledger.Shared.Commands;
using System;

namespace Ledger.Activations.Domain.Commands
{
    public class AttachCompanyDocumentsCommand : Command
    {
        public Guid ActivationId { get; set; }
        public string ContratoSocialPicture { get; set; }
        public string AlteracaoContratoSocialPicture { get; set; }
        public string OwnerDocumentPicture { get; set; }

        public override void Validate()
        {
        }
    }
}
