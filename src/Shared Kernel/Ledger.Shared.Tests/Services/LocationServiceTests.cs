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
    }
}
