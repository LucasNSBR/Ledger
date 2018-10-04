using Ledger.Shared.Entities.CityAggregate;
using Ledger.Shared.Locations.Specifications.CitySpecifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.Shared.Tests.Specifications.CitySpecifications
{
    [TestClass]
    public class CitySpecificationsTests
    {
        public Guid id;
        public Guid stateId;
        public City city;

        public CitySpecificationsTests()
        {
            id = new Guid("4dc761a0-37de-4023-8bf6-567462954ea9");
            stateId = new Guid("cad05ca7-7f96-4855-a876-b1805aaad263");
            city = new City(id, "Belo Horizonte", stateId);
        }

        [TestMethod]
        public void CityIdSpecificationShouldBeTrue()
        {
            CityIdSpecification specification = new CityIdSpecification(id);
            Assert.IsTrue(specification.IsSatisfiedBy(city));
        }

        [TestMethod]
        [DataRow("Belo Horizonte")]
        [DataRow("belo horizonte")]
        [DataRow("BELO HORIZONTE")]
        [DataRow("BeLo HoRiZoNtE")]
        [DataRow("BeLo")]
        public void CityNameSpecificationShouldBeTrue(string value)
        {
            CityNameSpecification specification = new CityNameSpecification(value);
            Assert.IsTrue(specification.IsSatisfiedBy(city));
        }

        [TestMethod]
        public void CityStateIdSpecificationShouldBeTrue()
        {
            CityStateIdSpecification specification = new CityStateIdSpecification(stateId);
            Assert.IsTrue(specification.IsSatisfiedBy(city));
        }
    }
}
