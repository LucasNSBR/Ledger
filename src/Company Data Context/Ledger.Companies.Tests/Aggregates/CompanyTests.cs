using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Shared.Notifications;
using Ledger.Shared.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Ledger.Companies.Tests.Aggregates
{
    [TestClass]
    public class CompanyTests
    {
        EmailAddress email = new EmailAddress("ledger@gmail.com");
        Cnpj cnpj = new Cnpj("15131000");
        InscricaoEstadual inscricao = new InscricaoEstadual("51113");
        Address address = new Address(452, "One Way Microsoft", "Centro", "Perto do Prédio Grande", "Nova Serrana", "MG", "35519000");
        PhoneNumber phone = new PhoneNumber("555 418 9220");
        Company company;
        Owner owner = new Owner("Lucas Pereira Campos", DateTime.Now.AddYears(-20), new Cpf("981.153.856-99"));

        public CompanyTests()
        {
            company = new Company("Ledger Activation", "No Description", email, cnpj, inscricao, owner);
        }

        [TestMethod]
        public void CompanyShouldBeInactive()
        {
            Assert.IsFalse(company.Active);
        }

        [TestMethod]
        public void ShouldChangeAddressFromCompany()
        {
            company.ChangeAddress(address);

            Assert.AreEqual("One Way Microsoft", company.Address.Street);
        }

        [TestMethod]
        public void ShouldChangePhoneFromCompany()
        {
            company.ChangePhone(phone);

            Assert.AreEqual("555 418 9220", company.Phone.Number);
        }

        [TestMethod]
        public void CompanyShouldBeActivated()
        {
            company.SetActive();

            Assert.IsTrue(company.Active);
        }

        [TestMethod]
        public void TwiceActivationShouldAddDomainErrorNotification()
        {
            company.SetActive();
            company.SetActive();

            DomainNotification notification = company.GetNotifications().First();

            Assert.IsNotNull(notification);
        }

        [TestMethod]
        public void CompanyShouldBeInactivated()
        {
            company.SetActive();
            company.SetInactive();

            Assert.IsFalse(company.Active);
        }
    }
}
