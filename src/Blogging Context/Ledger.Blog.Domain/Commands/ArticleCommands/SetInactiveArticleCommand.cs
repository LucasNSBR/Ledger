using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.Blog.Domain.Commands.ArticleCommands
{
    public class SetInactiveArticleCommand : Command
    {
        public Guid ArticleId { get; set; }

        public override void Validate()
        {
            new ValidationContract<SetInactiveArticleCommand, Guid>(this, command => command.ArticleId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);
        }
    }
}
