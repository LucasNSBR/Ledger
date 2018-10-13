using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.Blog.Domain.Commands.ArticleCommands
{
    public class RegisterArticleCommand : Command
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid CategoryId { get; set; }

        public override void Validate()
        {
            new ValidationContract<RegisterArticleCommand, string>(this, command => command.Slug)
                .NotEmpty()
                .MinLength(10)
                .MaxLength(250)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<RegisterArticleCommand, string>(this, command => command.Title)
                .NotEmpty()
                .MinLength(10)
                .MaxLength(250)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<RegisterArticleCommand, string>(this, command => command.Body)
                .NotEmpty()
                .MinLength(50)
                .MaxLength(5000)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<RegisterArticleCommand, Guid>(this, command => command.CategoryId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);
        }
    }
}
