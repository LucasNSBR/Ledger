﻿using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;

namespace Ledger.Identity.Domain.Commands.UserCommands
{
    public class RemoveUserFromRoleCommand : Command
    {
        public string Email { get; set; }
        public string RoleName { get; set; }

        public override void Validate()
        {
            new ValidationContract<RemoveUserFromRoleCommand, string>(this, command => command.Email)
                .NotEmpty()
                .Email()
                .MaxLength(150)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<RemoveUserFromRoleCommand, string>(this, command => command.RoleName)
                .NotEmpty()
                .Build()
                .AddToNotifier(this);
        }
    }
}
