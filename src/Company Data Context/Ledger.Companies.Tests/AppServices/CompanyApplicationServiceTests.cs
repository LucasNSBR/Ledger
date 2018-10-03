using Ledger.Companies.Application.AppServices.CompanyAppServices;
using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Companies.Domain.Commands;
using Ledger.Companies.Domain.Factories.CompanyFactories;
using Ledger.Companies.Tests.Mocks;
using Ledger.Shared.Locations.Services;
using Ledger.Shared.Notifications;
using Ledger.Shared.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Ledger.Companies.Tests.AppServices
{
    [TestClass]
    public class CompanyApplicationServiceTests
    {
        FakeCompanyRepository repository;
        LocationService locationService;
        FakeServiceBus bus;
        FakeUnitOfWork uow;
        CompanyApplicationService service;
        DomainNotificationHandler domainNotificationHandler;
        CompanyFactory factory;

        public CompanyApplicationServiceTests()
        {
            repository = new FakeCompanyRepository();
            locationService = new LocationService(new FakeCityRepository(), new StateFakeRepository(), new CountryFakeRepository());
            bus = new FakeServiceBus();
            uow = new FakeUnitOfWork();
            domainNotificationHandler = new DomainNotificationHandler();
            factory = new CompanyFactory();

            service = new CompanyApplicationService(repository, factory, locationService, domainNotificationHandler, uow, bus);
        }

        [TestMethod]
        public void ShouldGetById()
        {
            Company company = service.GetById(new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1"));

            Assert.IsNotNull(company);
        }

        [TestMethod]
        public void ShouldGetByCnpj()
        {
            Company company = service.GetByCnpj("59009518000141");

            Assert.IsNotNull(company);
        }

        [TestMethod]
        public void ShouldRegisterCompany()
        {
            RegisterCompanyCommand command = new RegisterCompanyCommand
            {
                Name = "Contoso University",
                Description = "Contoso corporation",
                Cnpj = "55888518000141",
                InscricaoEstadual = "115153",
                Email = "admin@contoso.com",
                OwnerName = "Satya Nadella",
                OwnerBirthday = DateTime.Now.AddYears(-30),
                OwnerCpf = "23854410099"
            };

            service.Register(command);

            Assert.AreEqual(2, repository.GetCount());
        }

        [TestMethod]
        public void ShouldFailOnRegisterCompany()
        {
            //Register with same CNPJ of the seed company 
            RegisterCompanyCommand command = new RegisterCompanyCommand
            {
                Name = "Microsoft Corporation",
                Description = "Microsoft corporation",
                Cnpj = "59009518000141",
                InscricaoEstadual = "115153",
                Email = "admin@microsoft.com",
                OwnerName = "Satya Nadella",
                OwnerBirthday = DateTime.Now.AddYears(-30),
                OwnerCpf = "23854410099"
            };

            service.Register(command);

            Assert.AreEqual("CNPJ duplicado", domainNotificationHandler.GetNotifications().First().Title);
        }

        [TestMethod]
        public void ShouldUpdateCompany()
        {
            UpdateCompanyCommand command = new UpdateCompanyCommand
            {
                CompanyId = new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1"),
                Name = "Microsoft Corporation",
                Description = "Microsoft corporation",
                Cnpj = "55888518000141",
                InscricaoEstadual = "115153",
                Email = "admin@microsoft.com",
                OwnerName = "Satya Nadella",
                OwnerBirthday = DateTime.Now.AddYears(-30),
                OwnerCpf = "23854410099"
            };

            service.Update(command);

            Company company = service.GetById(new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1"));

            Assert.AreEqual("Microsoft Corporation", company.Name);
            Assert.AreEqual("55888518000141", company.Cnpj.Number);
        }

        [TestMethod]
        public void ShouldFailOnUpdateCompany()
        {
            //Register a new company 
            RegisterCompanyCommand registerCommand = new RegisterCompanyCommand
            {
                Name = "Microsoft Corporation",
                Description = "Microsoft corporation",
                Cnpj = "59009518000141",
                InscricaoEstadual = "115153",
                Email = "admin@microsoft.com",
                OwnerName = "Satya Nadella",
                OwnerBirthday = DateTime.Now.AddYears(-30),
                OwnerCpf = "23854410099"
            };

            service.Register(registerCommand);

            //Change existing seed company to same CNPJ from new company
            UpdateCompanyCommand command = new UpdateCompanyCommand
            {
                CompanyId = new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1"),
                Name = "Microsoft Corporation",
                Description = "Microsoft corporation",
                Cnpj = "49009518000141",
                InscricaoEstadual = "115153",
                Email = "admin@microsoft.com",
                OwnerName = "Satya Nadella",
                OwnerBirthday = DateTime.Now.AddYears(-30),
                OwnerCpf = "23854410099"
            };

            service.Update(command);

            Assert.AreEqual("CNPJ duplicado", domainNotificationHandler.GetNotifications().First().Title);
        }

        [TestMethod]
        public void ShouldChangeCompanyPhone()
        {
            ChangeCompanyPhoneCommand command = new ChangeCompanyPhoneCommand
            {
                CompanyId = new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1"),
                PhoneNumber = "37 1533333113"
            };

            Company company = service.GetById(new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1"));
            Assert.AreEqual(null, company.Address);

            service.ChangePhone(command);
            Assert.AreEqual("37 1533333113", company.Phone.Number);
        }

        [TestMethod]
        public void ShouldChangeCompanyAddress()
        {
            ChangeCompanyAddressCommand command = new ChangeCompanyAddressCommand
            {
                Number = 22,
                Street = "One Way",
                CityId = Guid.NewGuid(),
                StateId = Guid.NewGuid(),
                CountryId = Guid.NewGuid(),
                Cep = "00112233",
                Complementation = "Central Square",
                Neighborhood = "Central",
                CompanyId = new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1")
            };

            Company company = service.GetById(new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1"));
            Assert.AreEqual(null, company.Address);

            service.ChangeAddress(command);
            Assert.AreEqual("00112233", company.Address.Cep);
        }


    }
}
