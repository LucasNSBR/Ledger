using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Companies.Domain.Commands;
using Ledger.Companies.Domain.Context;
using Ledger.Companies.Domain.Factories.CompanyFactories;
using Ledger.Companies.Domain.Repositories;
using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.Entities.CityAggregate;
using Ledger.Shared.Entities.CountryAggregate;
using Ledger.Shared.Entities.StateAggregate;
using Ledger.Shared.IntegrationEvents.Events.CompanyEvents;
using Ledger.Shared.Locations.Services;
using Ledger.Shared.Notifications;
using Ledger.Shared.ValueObjects;
using System;
using System.Collections.Generic;

namespace Ledger.Companies.Application.AppServices.CompanyAppServices
{
    public class CompanyApplicationService : BaseApplicationService, ICompanyApplicationService
    {
        private readonly ICompanyRepository _repository;
        private readonly ICompanyFactory _factory;
        private readonly ILocationService _locationService;

        public CompanyApplicationService(ICompanyRepository repository, ICompanyFactory factory, ILocationService locationService, IDomainNotificationHandler domainNotificationHandler, IUnitOfWork<ILedgerCompanyDbAbstraction> unitOfWork, IIntegrationServiceBus integrationBus) : base(domainNotificationHandler, unitOfWork, integrationBus)
        {
            _repository = repository;
            _factory = factory;
            _locationService = locationService;
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

            Company company = _factory.CreateCompany(command.Name, command.Description, command.Email, command.Cnpj,
                command.InscricaoEstadual, command.OwnerName, command.OwnerBirthday, command.OwnerCpf);

            if (NotifyCnpjExists(company.Cnpj, company.Id))
                return;

            _repository.Register(company);

            if (Commit())
                Publish(new RegisteredCompanyIntegrationEvent(company.Id, company.Email.Email));
        }

        public void Update(UpdateCompanyCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Company company = _factory.CreateCompany(command.Name, command.Description, command.Email, command.Cnpj,
                command.InscricaoEstadual, command.OwnerName, command.OwnerBirthday, command.OwnerCpf, companyId: command.CompanyId);

            if (NotifyCnpjExists(company.Cnpj, company.Id))
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

            LocationResult result = _locationService.TryGetLocation(command.CityId, command.StateId, command.CountryId);
            if (!result.Success)
            {
                AddNotifications((List<DomainNotification>)result.Notifications);
                return;
            }

            City city = result.City;
            State state = result.State;
            Country country = result.Country;

            Address address = new Address(command.Number, command.Street, command.Neighborhood, command.Complementation, city.Name, state.Name, country.Name, command.Cep);
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

        private bool NotifyCnpjExists(Cnpj cnpj, Guid id)
        {
            bool exists = _repository.CnpjExists(cnpj, id);

            if (exists)
            {
                AddNotification("CNPJ duplicado", "Uma empresa com o mesmo CNPJ já foi registrada.");
                return true;
            }

            return false;
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
