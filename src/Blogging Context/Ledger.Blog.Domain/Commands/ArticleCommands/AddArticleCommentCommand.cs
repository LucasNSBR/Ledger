using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.Blog.Domain.Commands.ArticleCommands
{
    public class AddArticleCommentCommand : Command
    {
        public Guid ArticleId { get; set; }
        public string Body { get; set; }

        public override void Validate()
        {
            new ValidationContract<AddArticleCommentCommand, Guid>(this, command => command.ArticleId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);

            new ValidationContract<AddArticleCommentCommand, string>(this, command => command.Body)
                .NotEmpty()
                .MinLength(1)
                .MaxLength(250)
                .Build()
                .AddToNotifier(this);
        }
    }
}
