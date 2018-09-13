using Ledger.CrossCutting.EmailService.Dispatchers;
using Ledger.CrossCutting.EmailService.Models;
using Ledger.CrossCutting.EmailService.Services.Factories;
using Ledger.Identity.Domain.Events.UserEvents;
using Ledger.Identity.Domain.Models.Aggregates.UserAggregate;
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
        private readonly IEmailFactory _emailFactory;
        private readonly IEmailDispatcher _emailDispatcher;
        private readonly LedgerUserManager _userManager;

        public UserDomainEventHandler(IEmailFactory emailFactory, IEmailDispatcher emailDispatcher, LedgerUserManager userManager)
        {
            _emailFactory = emailFactory;
            _emailDispatcher = emailDispatcher;
            _userManager = userManager;
        }

        public async Task Handle(UserRegisteredEvent @event)
        {
            string email = @event.Email;
            string confirmationToken = @event.EmailConfirmationToken;

            EmailTemplate template = _emailFactory.CreateAccountConfirmationEmail(email, confirmationToken);

            await _emailDispatcher.SendEmailAsync(template);
        }

        public async Task Handle(UserLoggedInEvent @event)
        {
            LedgerIdentityUser user = await _userManager.FindByIdAsync(@event.UserId.ToString());

            bool confirmedEmail = await _userManager.IsEmailConfirmedAsync(user);
            string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            if (!confirmedEmail)
            {
                EmailTemplate template = _emailFactory.CreateAccountConfirmationEmail(@event.Email, confirmationToken);
                await _emailDispatcher.SendEmailAsync(template);
            }
        }

        public async Task Handle(UserForgotPasswordEvent @event)
        {
            string email = @event.Email;
            string resetToken = @event.PasswordResetToken;

            EmailTemplate template = _emailFactory.CreatePasswordResetEmail(email, resetToken);

            await _emailDispatcher.SendEmailAsync(template);
        }

        public async Task Handle(UserResetedPasswordEvent @event)
        {
            EmailTemplate template = _emailFactory.CreatePostPasswordResetEmail(@event.Email);
            await _emailDispatcher.SendEmailAsync(template);
        }

        public async Task Handle(UserChangedPasswordEvent @event)
        {
            EmailTemplate template = _emailFactory.CreatePostPasswordChangeEmail(@event.Email);
            await _emailDispatcher.SendEmailAsync(template);
        }
    }
}
