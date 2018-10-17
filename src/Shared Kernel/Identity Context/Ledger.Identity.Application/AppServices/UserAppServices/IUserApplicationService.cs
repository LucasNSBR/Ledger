using Ledger.Identity.Domain.Aggregates.UserAggregate;
using Ledger.Identity.Domain.Commands.UserCommands;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ledger.Identity.Application.AppServices.UserAppServices
{
    public interface IUserApplicationService
    {
        IQueryable<LedgerIdentityUser> GetAllUsers();
        Task<LedgerIdentityUser> GetById(Guid id);
        Task<LedgerIdentityUser> GetByEmail(string email);
        Task<LedgerIdentityUser> Register(RegisterUserCommand command);
        Task ConfirmEmail(ConfirmUserEmailCommand command);
        Task ForgotPassword(ForgotUserPasswordCommand command);
        Task ResetPassword(ResetUserPasswordCommand command);
        Task ChangePassword(ChangeUserPasswordCommand command);
        Task AddToRole(AddUserToRoleCommand command);
        Task RemoveFromRole(RemoveUserFromRoleCommand command);
        Task<ClaimsIdentity> Login(LoginUserCommand command);
    }
}
