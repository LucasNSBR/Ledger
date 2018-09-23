using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.HelpDesk.Tests.Aggregates.TicketCategoryAggregate
{
    [TestClass]
    public class TicketCategoryTests
    {
        [TestMethod]
        public void TestTicketCategory()
        {
            Guid idOne = new Guid("3fe0fcc8-b00d-40ec-ac18-cbeb769ff216");

            TicketCategory ticketCategory = new TicketCategory(idOne, "Problemas de ativação");

            Assert.AreEqual(idOne, ticketCategory.Id);
            Assert.AreEqual("Problemas de ativação", ticketCategory.Name);
        }
    }
}
