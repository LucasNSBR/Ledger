using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.Blog.Domain.Commands.ArticleCommands
{
    public class SetActiveArticleCommand : Command
    {
        public Guid ArticleId { get; set; }

        public override void Validate()
        {
            new ValidationContract<SetActiveArticleCommand, Guid>(this, command => command.ArticleId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);
        }
    }
}
