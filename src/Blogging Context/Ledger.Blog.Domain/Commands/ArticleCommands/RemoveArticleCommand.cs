using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.Blog.Domain.Commands.ArticleCommands
{
    public class RemoveArticleCommand : Command
    {
        public Guid ArticleId { get; set; }

        public override void Validate()
        {
            new ValidationContract<RemoveArticleCommand, Guid>(this, command => command.ArticleId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);
        }
    }
}
