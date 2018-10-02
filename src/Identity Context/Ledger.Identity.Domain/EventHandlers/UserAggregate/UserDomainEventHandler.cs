using Ledger.CrossCutting.EmailService.Configuration;
using Ledger.CrossCutting.EmailService.Models;
using Ledger.CrossCutting.EmailService.Services.Dispatchers;
using Ledger.Identity.Domain.Aggregates.UserAggregate;
using Ledger.Identity.Domain.Events.UserEvents;
using Ledger.Identity.Domain.Models.Services.UserServices;
using Ledger.Shared.EventHandlers;
using System.Threading.Tasks;

namespace Ledger.Identity.Domain.EventHandlers.UserAggregate
{
    public class UserDomainEventHandler : IDomainEventHandler<UserRegisteredEvent>,
                                          IDomainEventHandler<UserLoggedInEvent>,
                                          IDomainEventHandler<UserForgotPasswordEvent>,
                                          IDomainEventHandler<UserResetedPasswordEvent>,
                                          IDomainEventHandler<UserChangedPasswordEvent>
    {
        private readonly IEmailDispatcher _emailDispatcher;
        private readonly LedgerUserManager _userManager;

        public UserDomainEventHandler(IEmailDispatcher emailDispatcher, LedgerUserManager userManager)
        {
            _emailDispatcher = emailDispatcher;
            _userManager = userManager;
        }

        public async Task Handle(UserRegisteredEvent @event)
        {
            string email = @event.Email;
            string confirmationToken = @event.EmailConfirmationToken;

            EmailTemplate template = new EmailTemplate(email)
                .SetTemplate(EmailTemplateTypes.UserRegistered)
                .AddSubstitution("-code-", confirmationToken);

            await _emailDispatcher.SendEmailAsync(template);
        }

        public async Task Handle(UserLoggedInEvent @event)
        {
            LedgerIdentityUser user = await _userManager.FindByIdAsync(@event.UserId.ToString());

            bool confirmedEmail = await _userManager.IsEmailConfirmedAsync(user);
            string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            if (!confirmedEmail)
            {
                EmailTemplate template = new EmailTemplate(@event.Email)
                    .SetTemplate(EmailTemplateTypes.UserLoggedIn)
                    .AddSubstitution("-code-", confirmationToken);

                await _emailDispatcher.SendEmailAsync(template);
            }
        }

        public async Task Handle(UserForgotPasswordEvent @event)
        {
            string email = @event.Email;
            string resetToken = @event.PasswordResetToken;

            EmailTemplate template = new EmailTemplate(@event.Email)
                    .SetTemplate(EmailTemplateTypes.ResetUserPassword)
                    .AddSubstitution("-code-", resetToken);

            await _emailDispatcher.SendEmailAsync(template);
        }

        public async Task Handle(UserResetedPasswordEvent @event)
        {
            EmailTemplate template = new EmailTemplate(@event.Email)
                    .SetTemplate(EmailTemplateTypes.UsedResetedPassword);
                    
            await _emailDispatcher.SendEmailAsync(template);
        }

        public async Task Handle(UserChangedPasswordEvent @event)
        {
            EmailTemplate template = new EmailTemplate(@event.Email)
                    .SetTemplate(EmailTemplateTypes.UserChangedPassword);

            await _emailDispatcher.SendEmailAsync(template);
        }
    }
}
