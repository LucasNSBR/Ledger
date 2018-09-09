using Ledger.CrossCutting.Data.Transactions;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.Notifications;

namespace Ledger.Companies.Application.AppServices.CompanyAppServices
{
    public class CompanyApplicationService : BaseApplicationService, ICompanyApplicationService
    {
        public CompanyApplicationService(IDomainNotificationHandler domainNotificationHandler, IUnitOfWork unitOfWork, IServiceBus serviceBus) : base(domainNotificationHandler, unitOfWork, serviceBus)
        {
        }
    }
}
