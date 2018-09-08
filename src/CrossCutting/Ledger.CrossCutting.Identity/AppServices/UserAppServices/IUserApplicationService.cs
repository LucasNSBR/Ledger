using Ledger.CrossCutting.Identity.Commands;
using Ledger.CrossCutting.Identity.Models.Users;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ledger.CrossCutting.Identity.AppServices.UserAppServices
{
    public interface IUserApplicationService
    {
        Task<LedgerIdentityUser> GetById(Guid id);
        Task<LedgerIdentityUser> GetByEmail(string email);
        Task<LedgerIdentityUser> Register(RegisterUserCommand command);
        Task ConfirmEmail(ConfirmUserEmailCommand command);
        Task ForgotPassword(ForgotUserPasswordCommand command);
        Task ResetPassword(ResetUserPasswordCommand command);
        Task ChangePassword(ChangeUserPasswordCommand command);
        Task<ClaimsIdentity> Login(LoginUserCommand command);
    }
}
