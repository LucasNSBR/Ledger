using Ledger.CrossCutting.EmailService.Models;
using System.Threading.Tasks;

namespace Ledger.CrossCutting.EmailService.Dispatchers
{
    public interface IEmailDispatcher
    {
        Task SendEmailAsync(EmailTemplate template);
        Task SendEmailAsync(string to, string subject, string body);
    }
}
