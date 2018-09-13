using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.Shared.Tests.Entities
{
    [TestClass]
    public class EntityTests 
    {
        [TestMethod]
        public void ShouldBeEqualEntities()
        {
            //Using Company from Activation Bounded Context as Example of Entity<T>
            Guid id = new Guid("1336d21e-e564-41ad-a953-028c88e5c9d1");

            Company companyOne = new Company(id);
            Company companyTwo = new Company(id);

            //Same id's = equal -> ASSERT IS TRUE
            Assert.IsTrue(companyOne.Equals(companyTwo));
            Assert.IsTrue(companyOne == companyTwo);

            companyTwo = null;

            //Company 2 is now null = different id's -> ASSERT IS FALSE
            Assert.IsFalse(companyOne.Equals(companyTwo));
            Assert.IsFalse(companyOne == companyTwo);
        }

        [TestMethod]
        public void ShouldBeDifferentEntities()
        {
            //Using Company from Activation Bounded Context as Example of Entity<T>
            Guid id = new Guid("1336d21e-e564-41ad-a953-028c88e5c9d1");
            Guid idTwo = new Guid("a552bcb4-5eaf-4ebb-b822-e6c82b3d2778");

            Company companyOne = new Company(id);
            Company companyTwo = new Company(idTwo);
            
            //Same id's = different -> ASSERT IS TRUE
            Assert.IsTrue(!companyOne.Equals(companyTwo));
            Assert.IsTrue(companyOne != companyTwo);

            companyTwo = null;

            //Company 2 is now null = different id's -> ASSERT REMAIN TRUE
            Assert.IsTrue(!companyOne.Equals(companyTwo));
            Assert.IsTrue(companyOne != companyTwo);
        }

        [TestMethod]
        public void ShouldHaveEqualEntityHashCodes()
        {
            //Using Company from Activation Bounded Context as Example of Entity<T>
            Guid id = new Guid("1336d21e-e564-41ad-a953-028c88e5c9d1");

            //Same id's = SAME HASH CODE
            Company companyOne = new Company(id);
            Company companyTwo = new Company(id);

            int hashOne = companyOne.GetHashCode();
            int hashTwo = companyTwo.GetHashCode();

            Assert.AreEqual(hashOne, hashTwo);
        }

        [TestMethod]
        public void ShouldHaveDifferentEntityHashCodes()
        {
            //Using Company from Activation Bounded Context as Example of Entity<T>
            Guid id = new Guid("1336d21e-e564-41ad-a953-028c88e5c9d1");
            Guid idTwo = new Guid("a552bcb4-5eaf-4ebb-b822-e6c82b3d2778");

            Company companyOne = new Company(id);
            Company companyTwo = new Company(idTwo);

            //different id's = DIFFERNT HASH CODES
            int hashOne = companyOne.GetHashCode();
            int hashTwo = companyTwo.GetHashCode();

            Assert.AreNotEqual(hashOne, hashTwo);
        }
    }
}
