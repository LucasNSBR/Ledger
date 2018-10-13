using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;

namespace Ledger.Blog.Domain.Commands.ArticleCategoryCommands
{
    public class RegisterArticleCategoryCommand : Command
    {
        public string Name { get; set; }

        public override void Validate()
        {
            new ValidationContract<RegisterArticleCategoryCommand, string>(this, command => command.Name)
                .NotEmpty()
                .MinLength(5)
                .MaxLength(100)
                .Build()
                .AddToNotifier(this);
        }
    }
}
