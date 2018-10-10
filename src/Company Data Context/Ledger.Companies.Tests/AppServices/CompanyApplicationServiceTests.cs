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
        FakeIdentityResolver identityResolver;
        CompanyApplicationService service;
        DomainNotificationHandler domainNotificationHandler;
        CompanyFactory factory;

        public CompanyApplicationServiceTests()
        {
            repository = new FakeCompanyRepository();
            locationService = new LocationService(new FakeCityRepository(), new StateFakeRepository(), new CountryFakeRepository());
            bus = new FakeServiceBus();
            uow = new FakeUnitOfWork();
            identityResolver = new FakeIdentityResolver();
            domainNotificationHandler = new DomainNotificationHandler();
            factory = new CompanyFactory();

            service = new CompanyApplicationService(repository, factory, locationService, identityResolver, domainNotificationHandler, uow, bus);
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
                CityId = new Guid("baa3b30e-8da9-4f3e-a0c3-fae93da05346"),
                StateId = new Guid("066ed615-e330-4e85-a79c-68e498b3d363"),
                CountryId = new Guid("43231eb6-2aab-463c-9286-93827dd0eb17"),
                Cep = "00112233",
                Complementation = "Central Square",
                Neighborhood = "Central",
                CompanyId = new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1")
            };

            Company company = service.GetById(new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1"));
            Assert.AreEqual(null, company.Address);

            service.ChangeAddress(command);
            Assert.AreEqual("00112233", company.Address.Cep);
            Assert.AreEqual("Redmond", company.Address.City);
            Assert.AreEqual("Washington", company.Address.State);
            Assert.AreEqual("United States of America", company.Address.Country);
        }

        [TestMethod]
        public void ShouldFailChangeCompanyAddressInvalidLocation()
        {
            ChangeCompanyAddressCommand command = new ChangeCompanyAddressCommand
            {
                Number = 22,
                Street = "One Way",
                CityId = new Guid("baa3b30e-8da9-4f3e-a0c3-fae93da05346"),
                StateId = new Guid("066ed615-e330-4e85-a79c-68e498b3d363"),
                CountryId = new Guid("00000000-0000-0000-0000-000000000009"),
                Cep = "00112233",
                Complementation = "Central Square",
                Neighborhood = "Central",
                CompanyId = new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1")
            };

            Company company = service.GetById(new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1"));
           
            service.ChangeAddress(command);

            Assert.AreEqual("Não foi possível encontrar a localização pelo Id.", domainNotificationHandler.GetNotifications().First().Description);
        }

        [TestMethod]
        public void ShouldFailChangeCompanyAddressIncompatibleStateCountry()
        {
            ChangeCompanyAddressCommand command = new ChangeCompanyAddressCommand
            {
                Number = 22,
                Street = "One Way",
                CityId = new Guid("baa3b30e-8da9-4f3e-a0c3-fae93da05346"),
                StateId = new Guid("066ed615-e330-4e85-a79c-68e498b3d363"),
                CountryId = new Guid("5aa5f409-dac4-42ee-8683-4f3087d81932"),
                Cep = "00112233",
                Complementation = "Central Square",
                Neighborhood = "Central",
                CompanyId = new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1")
            };

            Company company = service.GetById(new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1"));

            service.ChangeAddress(command);

            Assert.AreEqual("O estado especificado não pertence à nação.", domainNotificationHandler.GetNotifications().First().Description);
        }


        [TestMethod]
        public void ShouldFailChangeCompanyAddressIncompatibleCityState()
        {
            ChangeCompanyAddressCommand command = new ChangeCompanyAddressCommand
            {
                Number = 22,
                Street = "One Way",
                CityId = new Guid("7a73e15f-67ac-4e92-9434-bfe7ab32a2e4"),
                StateId = new Guid("066ed615-e330-4e85-a79c-68e498b3d363"),
                CountryId = new Guid("5aa5f409-dac4-42ee-8683-4f3087d81932"),
                Cep = "00112233",
                Complementation = "Central Square",
                Neighborhood = "Central",
                CompanyId = new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1")
            };

            Company company = service.GetById(new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1"));

            service.ChangeAddress(command);

            Assert.AreEqual("A cidade especificada não pertence ao estado.", domainNotificationHandler.GetNotifications().First().Description);
        }
    }
}
