using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.Blog.Domain.Commands.ArticleCategoryCommands
{
    public class UpdateArticleCategoryCommand : Command
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }

        public override void Validate()
        {
            new ValidationContract<UpdateArticleCategoryCommand, Guid>(this, command => command.CategoryId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);

            new ValidationContract<UpdateArticleCategoryCommand, string>(this, command => command.Name)
                .NotEmpty()
                .MinLength(5)
                .MaxLength(100)
                .Build()
                .AddToNotifier(this);
        }
    }
}
