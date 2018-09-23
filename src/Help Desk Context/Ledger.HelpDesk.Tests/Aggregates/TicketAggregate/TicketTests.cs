using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.HelpDesk.Domain.Aggregates.UserAggregate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
        public void ShouldAddMessageToTicket()
        {
            ticket.AddMessage("Quando irão resolver meu problema?", user);

            Assert.AreEqual(1, ticket.GetMessages().Count);
        }

        [TestMethod]
        public void ShouldGetMessagesFromUser()
        {
            ticket.AddMessage("Quando irão resolver meu problema?", user);

            Assert.AreEqual(1, ticket.GetMessagesFrom(user).Count);
        }
    }
}
