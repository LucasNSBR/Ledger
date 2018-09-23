using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Specifications.TicketCategorySpecifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.HelpDesk.Tests.Specifications.TicketCategorySpecifications
{
    [TestClass]
    public class TicketCategoryIdSpecificationTests
    {
        Guid idOne = new Guid("3fe0fcc8-b00d-40ec-ac18-cbeb769ff216");
        Guid idTwo = new Guid("d28d6da1-fb9c-49ab-84f9-c37ab3997cb8");

        [TestMethod]
        public void ShouldMatchUsingIdSpecification()
        {
            TicketCategory ticketCategory = new TicketCategory(idOne, "Problemas com o Login");
            TicketCategoryIdSpecification specificationOne = new TicketCategoryIdSpecification(idOne);
            TicketCategoryIdSpecification specificationTwo = new TicketCategoryIdSpecification(idTwo);

            bool resultTrue = specificationOne.IsSatisfiedBy(ticketCategory);
            bool resultFalse = specificationTwo.IsSatisfiedBy(ticketCategory);

            Assert.IsTrue(resultTrue);
            Assert.IsFalse(resultFalse);
        }
    }
}
