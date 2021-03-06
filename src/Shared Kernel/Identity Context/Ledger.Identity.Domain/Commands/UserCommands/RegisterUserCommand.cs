﻿using Ledger.Shared.Commands;
using Ledger.Shared.Extensions;
using LilValidation.Core;

namespace Ledger.Identity.Domain.Commands.UserCommands
{
    public class RegisterUserCommand : Command
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public override void Validate()
        {
            new ValidationContract<RegisterUserCommand, string>(this, command => command.Email)
                .NotEmpty()
                .Email()
                .MaxLength(150)
                .Build()
                .AddToNotifier(this);

            new ValidationContract<RegisterUserCommand, string>(this, command => command.Password)
                .NotEmpty()
                .MinLength(8)
                .MaxLength(24)
                .Build()
                .AddToNotifier(this);
        }
    }
}
