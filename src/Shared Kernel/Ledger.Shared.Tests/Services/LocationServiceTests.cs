using Ledger.Shared.Locations.Services;
using Ledger.Shared.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Ledger.Shared.Tests.Services
{
    [TestClass]
    public class LocationServiceTests
    {
        LocationService service = new LocationService(new FakeCityRepository(), new StateFakeRepository(), new CountryFakeRepository());

        [TestMethod]
        public void ShouldSuccessGetLocation()
        {
            Guid cityId = new Guid("7a73e15f-67ac-4e92-9434-bfe7ab32a2e4");
            Guid stateId = new Guid("3a68cfeb-34bc-43f8-8416-9f3d503f11a3");
            Guid countryId = new Guid("5aa5f409-dac4-42ee-8683-4f3087d81932");

            var result = service.TryGetLocation(cityId, stateId, countryId);

            Assert.IsTrue(result.Success);
            Assert.IsFalse(result.Notifications.Any());
        }

        [TestMethod]
        public void ShouldFailGetLocation()
        {
            Guid cityId = new Guid("7a73e15f-67ac-4e92-9434-bfe7ab32a2e4");
            Guid stateId = new Guid("3a68cfeb-34bc-43f8-8416-9f3d503f11a3");
            //Guid countryId = new Guid("5aa5f409-dac4-42ee-8683-4f3087d81932");

            var result = service.TryGetLocation(cityId, stateId, Guid.NewGuid());

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Notifications.Any());
        }

        [TestMethod]
        public void ShouldFailGetLocationAndReturnMessage()
        {
            Guid cityId = new Guid("7a73e15f-67ac-4e92-9434-bfe7ab32a2e4");
            Guid stateId = new Guid("3a68cfeb-34bc-43f8-8416-9f3d503f11a3");
            //Guid countryId = new Guid("5aa5f409-dac4-42ee-8683-4f3087d81932");

            var result = service.TryGetLocation(cityId, stateId, Guid.NewGuid());

            Assert.AreEqual("Não foi possível encontrar a localização pelo Id.", result.Notifications.First().Description);
        }

        [TestMethod]
        public void ShouldFailGetLocationIncompatibleLocations()
        {
            //Redmond
            Guid cityId = new Guid("baa3b30e-8da9-4f3e-a0c3-fae93da05346");
            //Washington
            Guid stateId = new Guid("066ed615-e330-4e85-a79c-68e498b3d363");
            //Brazil?! (WRONG!)
            Guid countryId = new Guid("5aa5f409-dac4-42ee-8683-4f3087d81932");

            var result = service.TryGetLocation(cityId, stateId, countryId);

            Assert.AreEqual("O estado especificado não pertence à nação.", result.Notifications.First().Description);
        }

        [TestMethod]
        public void ShouldGetLocationValues()
        {
            //Compatible City, State and Country (City is in state, state is in country)
            Guid cityId = new Guid("7a73e15f-67ac-4e92-9434-bfe7ab32a2e4");
            Guid stateId = new Guid("3a68cfeb-34bc-43f8-8416-9f3d503f11a3");
            Guid countryId = new Guid("5aa5f409-dac4-42ee-8683-4f3087d81932");

            var result = service.TryGetLocation(cityId, stateId, countryId);

            Assert.AreEqual("Belo Horizonte", result.City.Name);
            Assert.AreEqual("Minas Gerais", result.State.Name);
            Assert.AreEqual("Brazil", result.Country.Name);
        }
    }
}
