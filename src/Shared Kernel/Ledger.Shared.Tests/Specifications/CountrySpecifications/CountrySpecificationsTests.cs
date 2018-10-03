using Ledger.Shared.Entities.CountryAggregate;
using Ledger.Shared.Locations.Specifications.CountrySpecifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.Shared.Tests.Specifications.CountrySpecifications
{
    [TestClass]
    public class CountrySpecificationsTests
    {
        public Guid id;
        public Country country;

        public CountrySpecificationsTests()
        {
            id = new Guid("4dc761a0-37de-4023-8bf6-567462954ea9");
            country = new Country(id, "BR", "Brazil");
        }

        [TestMethod]
        public void CountryIdSpecificationShouldBeTrue()
        {
            CountryIdSpecification specification = new CountryIdSpecification(id);
            Assert.IsTrue(specification.IsSatisfiedBy(country));
        }

        [TestMethod]
        [DataRow("BraZIL")]
        [DataRow("BrazIL")]
        [DataRow("BRAZIL")]
        [DataRow("Brazil")]
        [DataRow("Bra")]
        public void CountryNameSpecificationShouldBeTrue(string value)
        {
            CountryNameSpecification specification = new CountryNameSpecification(value);
            Assert.IsTrue(specification.IsSatisfiedBy(country));
        }
    }
}
