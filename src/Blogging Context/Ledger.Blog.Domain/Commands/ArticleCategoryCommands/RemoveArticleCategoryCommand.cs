using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.Blog.Domain.Commands.ArticleCategoryCommands
{
    public class RemoveArticleCategoryCommand : Command
    {
        public Guid CategoryId { get; set; }

        public override void Validate()
        {
            new ValidationContract<RemoveArticleCategoryCommand, Guid>(this, command => command.CategoryId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);
        }
    }
}
