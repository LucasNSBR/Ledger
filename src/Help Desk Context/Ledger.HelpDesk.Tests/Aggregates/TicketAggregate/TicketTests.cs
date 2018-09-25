using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using Ledger.Shared.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Ledger.HelpDesk.Tests.Aggregates.TicketAggregate
{
    [TestClass]
    public class TicketTests
    {
        public Ticket ticket;
        public TicketCategory category;
        public TicketUser user;

        public TicketTests()
        {
            category = new TicketCategory("Problemas de ativação");
            user = new TicketUser(Guid.NewGuid(), "contoso@contoso.com");
            ticket = new Ticket("Não consigo anexar documentos", "Meus documentos falham ao serem anexados para enviar e ativar a conta", category, user);
        }

        [TestMethod]
        public void TicketShouldBeOpenedAtConstructor()
        {
            Assert.AreNotEqual(DateTime.MinValue, ticket.TicketStatus.DateOpened);
            Assert.IsTrue(ticket.IsOpened());
        }

        [TestMethod]
        public void TicketShouldAttachImage()
        {
            Image issuePrint = new Image(new byte[8]);
            ticket.AttachIssuePicture(issuePrint);
            
            Assert.IsNotNull(ticket.IssuePicture);
        }

        [TestMethod]
        public void TicketShouldAttachASupportUserToHelp()
        {
            //Come from repository
            SupportUser user = new SupportUser(Guid.NewGuid(), "support@contoso.com");

            ticket.AssignSupportUser(user);

            Assert.AreEqual(user, ticket.SupportUser);
            Assert.IsTrue(ticket.AlreadyHaveSupport());
        }

        [TestMethod]
        public void TicketShouldFailToAttachASupportUserToHelp()
        {
            //Come from repository
            SupportUser user = new SupportUser(Guid.NewGuid(), "support@contoso.com");

            ticket.AssignSupportUser(user);

            ticket.AssignSupportUser(user);

            Assert.AreEqual("Suporte já definido", ticket.GetNotifications().First().Title);
        }

        [TestMethod]
        public void TicketShouldBeClosed()
        {
            ticket.Close();

            Assert.AreNotEqual(DateTime.MinValue, ticket.TicketStatus.DateClosed);
            Assert.IsTrue(ticket.IsClosed());
        }

        [TestMethod]
        public void ShouldAddMessageFromUser()
        {
            ticket.AddUserMessage("Olá, mundo!");
            Assert.AreEqual(1, ticket.GetMessages().Count());  
        }

        [TestMethod]
        public void ShouldAddMessageFromSupport()
        {
            SupportUser user = new SupportUser(Guid.NewGuid(), "support@contoso.com");

            ticket.AssignSupportUser(user);

            ticket.AddSupportMessage("Olá, mundo!");
            Assert.AreEqual(1, ticket.GetMessages().Count());
        }

        [TestMethod]
        public void ShouldFailToAddMessageFromSupport()
        {
            ticket.AddSupportMessage("Olá, mundo!");
            Assert.AreEqual("Sem suporte", ticket.GetNotifications().First().Title);
        }

        [TestMethod]
        public void ShouldGetAllMessages()
        {
            SupportUser user = new SupportUser(Guid.NewGuid(), "support@contoso.com");
            ticket.AssignSupportUser(user);

            ticket.AddSupportMessage("Olá, como eu posso te ajudar?");
            ticket.AddUserMessage("Estou com um problema para ativar minha conta.");
            ticket.AddSupportMessage("Qual problema você está tendo?");
            ticket.AddUserMessage("Não consigo anexar meus documentos para efetuar a ativação.");
            ticket.AddSupportMessage("Um momento enquanto eu faço alguns testes.");
            ticket.AddSupportMessage("Pronto. Tente agora.");
            ticket.AddUserMessage("Consegui!");

            Assert.AreEqual(7, ticket.GetMessages().Count);
        }

        [TestMethod]
        public void ShouldGetUserMessages()
        {
            SupportUser user = new SupportUser(Guid.NewGuid(), "support@contoso.com");
            ticket.AssignSupportUser(user);

            ticket.AddSupportMessage("Olá, como eu posso te ajudar?");
            ticket.AddUserMessage("Estou com um problema para ativar minha conta.");
            ticket.AddSupportMessage("Qual problema você está tendo?");
            ticket.AddUserMessage("Não consigo anexar meus documentos para efetuar a ativação.");
            ticket.AddSupportMessage("Um momento enquanto eu faço alguns testes.");
            ticket.AddSupportMessage("Pronto. Tente agora.");
            ticket.AddUserMessage("Consegui!");

            Assert.AreEqual(3, ticket.GetUserMessages().Count);
        }

        [TestMethod]
        public void ShouldGetSupportMessages()
        {
            SupportUser user = new SupportUser(Guid.NewGuid(), "support@contoso.com");
            ticket.AssignSupportUser(user);

            ticket.AddSupportMessage("Olá, como eu posso te ajudar?");
            ticket.AddUserMessage("Estou com um problema para ativar minha conta.");
            ticket.AddSupportMessage("Qual problema você está tendo?");
            ticket.AddUserMessage("Não consigo anexar meus documentos para efetuar a ativação.");
            ticket.AddSupportMessage("Um momento enquanto eu faço alguns testes.");
            ticket.AddSupportMessage("Pronto. Tente agora.");
            ticket.AddUserMessage("Consegui!");

            Assert.AreEqual(4, ticket.GetSupportMessages().Count);
        }
    }
}
