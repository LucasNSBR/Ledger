using Ledger.Shared.Entities.CityAggregate;
using Ledger.Shared.Entities.StateAggregate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.Shared.Tests.Entities.Locations
{
    [TestClass]
    public class CityTests
    {
        public City city;
        public State state;

        public CityTests()
        {
            state = new State("MG", "Minas Gerais", new Guid("3a68cfeb-34bc-43f8-8416-9f3d503f11a3"));
            city = new City("Minas Gerais", state.Id);
        }

        [TestMethod]
        public void ShouldIsInCountry()
        {
            Assert.IsTrue(city.IsInState(state));
        }

        [TestMethod]
        public void ShouldFailIsInCountry()
        {
            city = new City(city.Name, Guid.NewGuid());
            Assert.IsFalse(city.IsInState(state));
        }
    }
}
