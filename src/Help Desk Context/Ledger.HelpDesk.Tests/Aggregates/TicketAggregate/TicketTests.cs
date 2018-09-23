using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.HelpDesk.Tests.Aggregates.TicketAggregate
{
    [TestClass]
    public class TicketTests
    {
        public Ticket ticket;
        public TicketCategory category;

        public TicketTests()
        {
            category = new TicketCategory("Problemas de ativação");
            ticket = new Ticket("Não consigo anexar documentos", "Meus documentos falham ao serem anexados para enviar e ativar a conta", category);
        }

        [TestMethod]
        public void ShouldAddMessageToTicket()
        {
            TicketUser user = new TicketUser(Guid.NewGuid());
            TicketMessage message = new TicketMessage("Quando irão resolver meu problema?", user);

            ticket.AddMessage(message);

            Assert.AreEqual(1, ticket.GetMessages().Count);
        }

        [TestMethod]
        public void ShouldGetMessagesFromUser()
        {
            TicketUser user = new TicketUser(Guid.NewGuid());
            TicketMessage message = new TicketMessage("Quando irão resolver meu problema?", user);

            ticket.AddMessage(message);

            Assert.AreEqual(1, ticket.GetMessagesFrom(user).Count);
        }
    }
}
