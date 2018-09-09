using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Companies.Domain.Commands;
using Ledger.Companies.Domain.Repositories;
using Ledger.CrossCutting.Data.Transactions;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.Notifications;
using Ledger.Shared.ValueObjects;
using System;

namespace Ledger.Companies.Application.AppServices.CompanyAppServices
{
    public class CompanyApplicationService : BaseApplicationService, ICompanyApplicationService
    {
        private readonly ICompanyRepository _repository;

        public CompanyApplicationService(ICompanyRepository repository, IDomainNotificationHandler domainNotificationHandler, IUnitOfWork unitOfWork, IServiceBus serviceBus) : base(domainNotificationHandler, unitOfWork, serviceBus)
        {
            _repository = repository;
        }

        public Company GetByCnpj(string cnpj)
        {
            return _repository.GetByCnpj(cnpj);
        }

        public Company GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void Register(RegisterCompanyCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            EmailAddress email = new EmailAddress(command.Email);
            Cnpj cnpj = new Cnpj(command.Cnpj);
            InscricaoEstadual inscricaoEstadual = new InscricaoEstadual(command.InscricaoEstadual);
            Company company = new Company(command.Name, command.Description, email, cnpj, inscricaoEstadual);

            _repository.Register(company);

            Commit();
        }

        public void Update(UpdateCompanyCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            EmailAddress email = new EmailAddress(command.Email);
            Cnpj cnpj = new Cnpj(command.Cnpj);
            InscricaoEstadual inscricaoEstadual = new InscricaoEstadual(command.InscricaoEstadual);
            Company company = new Company(command.Id, command.Name, command.Description, email, cnpj, inscricaoEstadual);

            if (NotifyCnpjExists(command.Cnpj))
                return;

            _repository.Update(company);

            Commit();
        }

        public void ChangeAddress(ChangeCompanyAddressCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Company company = _repository.GetById(command.CompanyId);

            if (NotifyNullCompany(company))
                return;

            Address address = new Address(command.Number, command.State, command.Neighborhood, command.Complementation, command.City, command.State, command.Cep);
            company.ChangeAddress(address);

            if (AddNotifications(company))
                return;

            _repository.Update(company);

            Commit();
        }

        public void ChangePhone(ChangeCompanyPhoneCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Company company = _repository.GetById(command.CompanyId);

            if (NotifyNullCompany(company))
                return;

            PhoneNumber phone = new PhoneNumber(command.PhoneNumber);
            company.ChangePhone(phone);

            if (AddNotifications(company))
                return;

            _repository.Update(company);

            Commit();
        }

        private bool NotifyCnpjExists(string cnpj)
        {
            Company company = _repository.GetByCnpj(cnpj);

            if (company != null)
            {
                AddNotification("CNPJ duplicado", "Uma empresa com o mesmo CNPJ já foi registrada.");
                return true;
            }

            return true;
        }

        private bool NotifyNullCompany(Company company)
        {
            if (company == null)
            {
                AddNotification("Id inválido", "A empresa não pôde ser encontrada.");
                return true;
            }

            return false;
        }
    }
}
