using Ledger.CrossCutting.EmailService.Models;
using System.Threading.Tasks;

namespace Ledger.CrossCutting.EmailService.Services.Dispatchers
{
    public interface IEmailDispatcher
    {
        Task SendEmailAsync(EmailTemplate template);
        Task SendEmailAsync(string to, string subject, string body);
    }
}
