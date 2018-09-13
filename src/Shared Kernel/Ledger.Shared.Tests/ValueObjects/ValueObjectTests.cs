using Ledger.Shared.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ledger.Shared.Tests.ValueObjects
{
    [TestClass]
    public class ValueObjectTests
    {
        [TestMethod]
        public void ShouldBeEqualValueObjects()
        {
            string phone1 = "9000-4000";
            string phone2 = "9000-4000";

            PhoneNumber phone = new PhoneNumber(phone1);
            PhoneNumber phoneTwo = new PhoneNumber(phone2);

            Assert.IsTrue(phoneTwo.Equals(phone));
            Assert.IsTrue(phoneTwo == phone);

            phone = null;

            Assert.IsFalse(phoneTwo.Equals(phone));
            Assert.IsFalse(phoneTwo == phone);

            phone = phoneTwo;

            Assert.IsTrue(phoneTwo == phone);
        }

        [TestMethod]
        public void ShouldBeDifferentValueObjects()
        {
            string phone1 = "9000-4000";
            string phone2 = "0000-0000";

            PhoneNumber phone = new PhoneNumber(phone1);
            PhoneNumber phoneTwo = new PhoneNumber(phone2);

            Assert.IsFalse(phoneTwo.Equals(phone));
            Assert.IsTrue(phoneTwo != phone);

            phone = null;

            Assert.IsFalse(phoneTwo.Equals(phone));
            Assert.IsTrue(phoneTwo != phone);

            phone = phoneTwo;

            Assert.IsFalse(phoneTwo != phone);
        }

        [TestMethod]
        [DataRow("9111-4990", "9111-4990")]
        [DataRow("9128-4120", "9128-4120")]
        [DataRow("0000-0000", "0000-0000")]
        [DataRow("9148-0000", "9148-0000")]
        [DataRow("9148-0000", "9148-0000")]
        [DataRow(null, null)]
        [DataRow("1336d21e-e564-41ad-a953-028c88e5c9d1", "1336d21e-e564-41ad-a953-028c88e5c9d1")]
        public void ShouldHaveEqualHashCodes(string phone1, string phone2)
        {
            //using phone as example of ValueObject<T>
            PhoneNumber phone = new PhoneNumber(phone1);
            PhoneNumber phoneTwo = new PhoneNumber(phone2);

            //They should to have same object hash codes if property values are the same
            int phoneHash = phone.GetHashCode();
            int phoneTwoHash = phoneTwo.GetHashCode();

            Assert.AreEqual(phoneHash, phoneTwoHash);
        }

        [TestMethod]
        [DataRow("9265-4810", "9124-1000")]
        [DataRow("Data tests", "0000-0000")]
        [DataRow(null, "Telefone")]
        [DataRow("Data tests", "adfsuifdahh")]
        [DataRow("1", "0")]
        [DataRow("2", "3")]
        [DataRow("4", "5")]
        [DataRow("a552bcb4-5eaf-4ebb-b822-e6c82b3d2778", "1336d21e-e564-41ad-a953-028c88e5c9d1")]
        public void ShouldHaveDifferentHashCodes(string phone1, string phone2)
        {
            //using phone as example of ValueObject<T>
            PhoneNumber phone = new PhoneNumber(phone1);
            PhoneNumber phoneTwo = new PhoneNumber(phone2);

            //They should to have different object hash codes if property values are the same
            int phoneHash = phone.GetHashCode();
            int phoneTwoHash = phoneTwo.GetHashCode();

            Assert.AreNotEqual(phoneHash, phoneTwoHash);
        }
    }
}
