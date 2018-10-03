using Ledger.Shared.Entities.StateAggregate;
using Ledger.Shared.Locations.Specifications.StateSpecifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.Shared.Tests.Specifications.StateSpecifications
{
    [TestClass]
    public class StateSpecificationTests
    {
        public Guid id;
        public Guid countryId;
        public State state;

        public StateSpecificationTests()
        {
            id = new Guid("4dc761a0-37de-4023-8bf6-567462954ea9");
            countryId = new Guid("cad05ca7-7f96-4855-a876-b1805aaad263");
            state = new State(id, "MG", "Minas Gerais", countryId);
        }

        [TestMethod]
        public void StateIdSpecificationShouldBeTrue()
        {
            StateIdSpecification specification = new StateIdSpecification(id);
            Assert.IsTrue(specification.IsSatisfiedBy(state));
        }

        [TestMethod]
        [DataRow("Minas Gerais")]
        [DataRow("MINAS GERAIS")]
        [DataRow("Minas GERAIS")]
        [DataRow("Minas")]
        [DataRow("M")]
        public void StateNameSpecificationShouldBeTrue(string value)
        {
            StateNameSpecification specification = new StateNameSpecification(value);
            Assert.IsTrue(specification.IsSatisfiedBy(state));
        }

        [TestMethod]
        public void StateCountryIdSpecificationShouldBeTrue()
        {
            StateCountryIdSpecification specification = new StateCountryIdSpecification(countryId);
            Assert.IsTrue(specification.IsSatisfiedBy(state));
        }
    }
}
