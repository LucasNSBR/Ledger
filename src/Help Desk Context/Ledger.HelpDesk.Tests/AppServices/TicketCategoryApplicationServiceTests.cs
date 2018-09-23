using Ledger.HelpDesk.Application.AppServices.TicketCategoryAppServices;
using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Commands.TicketCategoryCommands;
using Ledger.HelpDesk.Tests.Mocks;
using Ledger.Shared.Notifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Ledger.HelpDesk.Tests.AppServices
{
    [TestClass]
    public class TicketCategoryApplicationServiceTests
    {
        Guid idOne = new Guid("3fe0fcc8-b00d-40ec-ac18-cbeb769ff216");

        public TicketCategoryApplicationService appService;
        public FakeTicketCategoryRepository repository;
        public DomainNotificationHandler domainNotificationHandler;
        public FakeServiceBus serviceBus;
        public FakeUnitOfWork unitOfWork;

        public TicketCategoryApplicationServiceTests()
        {
            repository = new FakeTicketCategoryRepository();
            domainNotificationHandler = new DomainNotificationHandler();
            serviceBus = new FakeServiceBus();
            unitOfWork = new FakeUnitOfWork();

            appService = new TicketCategoryApplicationService(repository, domainNotificationHandler, unitOfWork, serviceBus);
        }

        public void PopulateRepository()
        {
            TicketCategory ticketCategory = new TicketCategory(idOne, "Problemas de ativação");

            repository.Register(ticketCategory);
        }

        [TestMethod]
        public void ShouldGetAll()
        {
            PopulateRepository();
            PopulateRepository();
            PopulateRepository();

            Assert.AreEqual(3, appService.GetAllCategories().Count());
        }

        [TestMethod]
        public void ShouldGetById()
        {
            PopulateRepository();

            TicketCategory category = appService.GetById(idOne);

            Assert.AreEqual("Problemas de ativação", category.Name);
        }

        [TestMethod]
        public void ShouldRegisterTicketCategory()
        {
            RegisterTicketCategoryCommand command = new RegisterTicketCategoryCommand()
            {
                Name = "Problemas de na criação de ordens"
            };

            appService.Register(command);

            Assert.AreEqual(1, appService.GetAllCategories().Count());
        }

        [TestMethod]
        public void ShouldUpdateTicketCategory()
        {
            PopulateRepository();

            UpdateTicketCategoryCommand command = new UpdateTicketCategoryCommand()
            {
                Id = idOne,
                Name = "Problemas de na criação de ordens"
            };

            appService.Update(command);

            TicketCategory category = appService.GetById(idOne);

            Assert.AreEqual("Problemas de na criação de ordens", category.Name);
        }
    }
}
