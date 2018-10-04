using Ledger.Shared.Entities.CountryAggregate;
using Ledger.Shared.Entities.StateAggregate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.Shared.Tests.Entities.Locations
{
    [TestClass]
    public class StateTests
    {
        public State state;
        public Country country;

        public StateTests()
        {
            country = new Country(new Guid("3a68cfeb-34bc-43f8-8416-9f3d503f11a3"), "BR", "BRAZIL");
            state = new State("MG", "Minas Gerais", country.Id);
        }

        [TestMethod]
        public void ShouldIsInCountry()
        {
            Assert.IsTrue(state.IsInCountry(country));
        }

        [TestMethod]
        public void ShouldFailIsInCountry()
        {
            state = new State(state.ShortName, state.Name, Guid.NewGuid());
            Assert.IsFalse(state.IsInCountry(country));
        }
    }
}
