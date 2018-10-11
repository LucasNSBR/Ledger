using Ledger.HelpDesk.Application.AppServices.TicketAppServices;
using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.HelpDesk.Domain.Commands.TicketCommands;
using Ledger.HelpDesk.Domain.Factories;
using Ledger.HelpDesk.Tests.Mocks;
using Ledger.Shared.Notifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Ledger.HelpDesk.Tests.AppServices
{
    [TestClass]
    public class TicketApplicationServiceTests
    {
        FakeTicketRepository fakeTicketRepository;
        FakeTicketCategoryRepository fakeTicketCategoryRepository;
        DomainNotificationHandler domainNotificationHandler;
        TicketFactory factory;
        FakeServiceBus fakeServiceBus;
        FakeUnitOfWork uow;
        FakeIdentityResolver identityResolver;
        TicketApplicationService applicationService;

        public TicketApplicationServiceTests()
        {
            fakeTicketRepository = new FakeTicketRepository();
            fakeTicketCategoryRepository = new FakeTicketCategoryRepository();
            domainNotificationHandler = new DomainNotificationHandler();
            factory = new TicketFactory();
            fakeServiceBus = new FakeServiceBus();
            uow = new FakeUnitOfWork();
            identityResolver = new FakeIdentityResolver();

            applicationService = new TicketApplicationService(fakeTicketRepository, fakeTicketCategoryRepository, factory, identityResolver, domainNotificationHandler, uow, fakeServiceBus);
        }

        [TestMethod]
        public void ShouldGetAllTickets()
        {
            IQueryable<Ticket> ticket = applicationService.GetAllTickets();

            Assert.AreEqual(1, ticket.Count());
        }

        [TestMethod]
        public void ShouldGetAllTicketsByUserId()
        {
            Guid id = identityResolver.GetUserId();

            IQueryable<Ticket> ticket = applicationService.GetByUserId(id);
        }

        [TestMethod]
        public void ShouldFailToGetAllTicketsByUserIdAndAddNotification()
        {
            Guid id = Guid.NewGuid();
            IQueryable<Ticket> ticket = applicationService.GetByUserId(id);

            Assert.AreEqual(0, ticket.Count());
            Assert.AreEqual("Erro ao buscar", domainNotificationHandler.GetNotifications().First().Title);
        }

        [TestMethod]
        public void ShouldGetById()
        {
            Ticket ticket = applicationService.GetById(new Guid("36f90131-8ab3-4764-a56c-2ee78284562f"));

            Assert.IsNotNull(ticket);
        }

        [TestMethod]
        public void ShouldFailGetByIdAndAddNotification()
        {
            identityResolver.RefreshId();

            Ticket ticket = applicationService.GetById(new Guid("36f90131-8ab3-4764-a56c-2ee78284562f"));

            Assert.AreEqual("Usuário inválido", domainNotificationHandler.GetNotifications().First().Title);
        }

        [TestMethod]
        public void ShouldRegisterTicket()
        {
            Guid categoryId = new Guid("3fe0fcc8-b00d-40ec-ac18-cbeb769ff216");

            fakeTicketCategoryRepository.Register(new TicketCategory(categoryId, "Problemas"));

            RegisterTicketCommand command = new RegisterTicketCommand
            {
                CategoryId = categoryId,
                Title = "Olá, mundo!",
                Details = "Isso funciona"
            };

            applicationService.Register(command);

            Assert.AreEqual(2, applicationService.GetAllTickets().Count());
        }

        [TestMethod]
        public void ShouldAttachIssuePicture()
        {
            string base64 = "bHVjYXM=";

            AttachIssuePictureCommand command = new AttachIssuePictureCommand
            {
                TicketId = new Guid("36f90131-8ab3-4764-a56c-2ee78284562f"),
                IssuePicture = base64
            };

            applicationService.AttachIssuePicture(command);

            Ticket ticket = applicationService.GetById(new Guid("36f90131-8ab3-4764-a56c-2ee78284562f"));

            Assert.IsNotNull(ticket.IssuePicture);
        }

        [TestMethod]
        public void ShouldFailToAttachIssuePictureAndAddNotification()
        {
            identityResolver.RefreshId();

            string base64 = "bHVjYXM=";

            AttachIssuePictureCommand command = new AttachIssuePictureCommand
            {
                TicketId = new Guid("36f90131-8ab3-4764-a56c-2ee78284562f"),
                IssuePicture = base64
            };

            applicationService.AttachIssuePicture(command);

            Assert.AreEqual("Erro ao anexar", domainNotificationHandler.GetNotifications().First().Title);
        }

        [TestMethod]
        public void ShouldAddMessage()
        {
            AddMessageCommand command = new AddMessageCommand
            {
                TicketId = new Guid("36f90131-8ab3-4764-a56c-2ee78284562f"),
                Body = "Olá!"
            };

            applicationService.AddMessage(command);

            Ticket ticket = applicationService.GetById(new Guid("36f90131-8ab3-4764-a56c-2ee78284562f"));

            Assert.AreEqual(1, ticket.GetMessages().Count);
        }


        [TestMethod]
        public void ShouldFailToAddMessageAndAddNotification()
        {
            identityResolver.RefreshId();
            
            AddMessageCommand command = new AddMessageCommand
            {
                TicketId = new Guid("36f90131-8ab3-4764-a56c-2ee78284562f"),
                Body = "Olá!"
            };

            applicationService.AddMessage(command);

            Ticket ticket = applicationService.GetById(new Guid("36f90131-8ab3-4764-a56c-2ee78284562f"));

            Assert.AreEqual("Não possui acesso às mensagens", domainNotificationHandler.GetNotifications().First().Title);
        }

        [TestMethod]
        public void ShouldCloseTicket()
        {
            CloseTicketCommand command = new CloseTicketCommand
            {
                TicketId = new Guid("36f90131-8ab3-4764-a56c-2ee78284562f")
            };

            applicationService.Close(command);

            Ticket ticket = applicationService.GetById(new Guid("36f90131-8ab3-4764-a56c-2ee78284562f"));

            Assert.AreEqual(Status.Closed, ticket.TicketStatus.Status);
        }

        [TestMethod]
        public void ShouldFailToCloseTicketAndAddNotification()
        {
            CloseTicketCommand command = new CloseTicketCommand
            {
                TicketId = new Guid("36f90131-8ab3-4764-a56c-2ee78284562f")
            };

            applicationService.Close(command);
            applicationService.Close(command);

            Assert.AreEqual("Ticket finalizado", domainNotificationHandler.GetNotifications().First().Title);

        }
    }
}
