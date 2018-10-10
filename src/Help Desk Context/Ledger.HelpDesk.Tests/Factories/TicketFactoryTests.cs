using Ledger.CrossCutting.Identity.Aggregates.UserAggregate;
using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.HelpDesk.Domain.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.HelpDesk.Tests.Factories
{
    [TestClass]
    public class TicketFactoryTests
    {
        [TestMethod]
        public void FactoryShouldReturnTicket()
        {
            Guid catId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            TicketFactory factory = new TicketFactory();

            //Come from repository
            TicketCategory category = new TicketCategory(catId, "Problemas de ativação");

            //Come from repository
            User user = new User(userId);

            Ticket ticket = factory.OpenTicket("Não consigo ativar minha conta", "Falha ao ativar minha conta", category.Id, user.Id);

            Assert.IsNotNull(ticket);
            Assert.AreEqual(userId, user.Id);
            Assert.AreEqual(catId, category.Id);
        }
    }
}
