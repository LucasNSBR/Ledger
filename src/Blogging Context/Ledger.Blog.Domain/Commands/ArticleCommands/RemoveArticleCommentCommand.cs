using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;
using System;

namespace Ledger.Blog.Domain.Commands.ArticleCommands
{
    public class RemoveArticleCommentCommand : Command
    {
        public Guid ArticleId { get; set; }
        public Guid CommentId { get; set; }

        public override void Validate()
        {
            new ValidationContract<RemoveArticleCommentCommand, Guid>(this, command => command.ArticleId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);

            new ValidationContract<RemoveArticleCommentCommand, Guid>(this, command => command.CommentId)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);
        }
    }
}
