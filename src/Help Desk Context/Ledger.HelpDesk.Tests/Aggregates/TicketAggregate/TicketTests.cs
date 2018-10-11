using Ledger.CrossCutting.Identity.Aggregates.UserAggregate;
using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
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
        public User user;

        public TicketTests()
        {
            Guid userId = Guid.NewGuid();

            category = new TicketCategory("Problemas de ativação");
            user = new User(userId);
            ticket = new Ticket("Não consigo anexar documentos", "Meus documentos falham ao serem anexados para enviar e ativar a conta", category.Id, user.Id);
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
            User user = new User(Guid.NewGuid());

            ticket.AssignSupportUser(user.Id);

            Assert.AreEqual(user.Id, ticket.SupportUserId);
            Assert.IsTrue(ticket.AlreadyHaveSupport());
        }

        [TestMethod]
        public void TicketShouldFailToAttachASupportUserToHelp()
        {
            //Come from repository
            User user = new User(Guid.NewGuid());
           
            ticket.AssignSupportUser(user.Id);

            ticket.AssignSupportUser(user.Id);


            Assert.AreEqual("Suporte já definido", ticket.GetNotifications().First().Title);
        }

        [TestMethod]
        public void TicketShouldBeClosed()
        {
            ticket.Close();

            Assert.AreNotEqual(DateTime.MinValue, ticket.TicketStatus.DateClosed);
            Assert.IsTrue(!ticket.IsOpened());
        }

        [TestMethod]
        public void ShouldFailToCloseTicketAfterTicketClosed()
        {
            ticket.Close();
            ticket.Close();

            Assert.AreEqual("Ticket finalizado", ticket.GetNotifications().First().Title);
        }

        [TestMethod]
        public void ShouldAddMessageFromUser()
        {
            ticket.AddMessage("Olá, mundo!", user.Id);
            Assert.AreEqual(1, ticket.GetMessages().Count());
        }

        [TestMethod]
        public void ShouldFailToAddMessageAfterTicketClosed()
        {
            ticket.Close();
            ticket.AddMessage("Olá, mundo!", user.Id);

            Assert.AreEqual("Ticket finalizado", ticket.GetNotifications().First().Title);
        }

        [TestMethod]
        public void ShouldFailToAddMessageFromSupport()
        {
            User user = new User(Guid.NewGuid());

            ticket.AddMessage("Olá, mundo!", user.Id);
            Assert.AreEqual("Não possui acesso às mensagens", ticket.GetNotifications().First().Title);
        }

        [TestMethod]
        public void ShouldGetMessages()
        {
            User supportUser = new User(Guid.NewGuid());
            
            ticket.AssignSupportUser(supportUser.Id);

            ticket.AddMessage("Olá, como eu posso te ajudar?", supportUser.Id);
            ticket.AddMessage("Estou com um problema para ativar minha conta.", user.Id);
            ticket.AddMessage("Qual problema você está tendo?", supportUser.Id);
            ticket.AddMessage("Não consigo anexar meus documentos para efetuar a ativação.", user.Id);
            ticket.AddMessage("Um momento enquanto eu faço alguns testes.", supportUser.Id);
            ticket.AddMessage("Pronto. Tente agora.", supportUser.Id);
            ticket.AddMessage("Consegui!", user.Id);

            Assert.AreEqual(7, ticket.GetMessages().Count);
        }

        [TestMethod]
        public void ShouldGetMessagesFromUser()
        {
            User supportUser = new User(Guid.NewGuid());
           
            ticket.AssignSupportUser(supportUser.Id);

            ticket.AddMessage("Olá, como eu posso te ajudar?", supportUser.Id);
            ticket.AddMessage("Estou com um problema para ativar minha conta.", user.Id);
            ticket.AddMessage("Qual problema você está tendo?", supportUser.Id);
            ticket.AddMessage("Não consigo anexar meus documentos para efetuar a ativação.", user.Id);
            ticket.AddMessage("Um momento enquanto eu faço alguns testes.", supportUser.Id);
            ticket.AddMessage("Pronto. Tente agora.", supportUser.Id);
            ticket.AddMessage("Consegui!", user.Id);

            Assert.AreEqual(3, ticket.GetMessages(user.Id).Count);
        }
    }
}
