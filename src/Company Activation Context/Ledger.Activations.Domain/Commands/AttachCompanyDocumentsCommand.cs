using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.Activations.Domain.Commands
{
    public class AttachCompanyDocumentsCommand : Command
    {
        public Guid ActivationId { get; set; }
        public string ContratoSocialPicture { get; set; }
        public string AlteracaoContratoSocialPicture { get; set; }
        public string OwnerDocumentPicture { get; set; }
        public string ExtraDocument { get; set; }

        public override void Validate()
        {
            string base64RegexPattern = "^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{4}|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)$";

            new ValidationContract<AttachCompanyDocumentsCommand, Guid>(this, command => command.ActivationId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);

            new ValidationContract<AttachCompanyDocumentsCommand, string>(this, command => command.ContratoSocialPicture)
                .NotEmpty()
                .Regex(base64RegexPattern)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<AttachCompanyDocumentsCommand, string>(this, command => command.AlteracaoContratoSocialPicture)
                .NotEmpty()
                .Regex(base64RegexPattern)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<AttachCompanyDocumentsCommand, string>(this, command => command.OwnerDocumentPicture)
                .NotEmpty()
                .Regex(base64RegexPattern)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<AttachCompanyDocumentsCommand, string>(this, command => command.ExtraDocument)
                .NotEmpty()
                .Regex(base64RegexPattern)
                .Build()
                .AddToNotifier(this);
        }
    }
}
