using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Companies.Domain.Factories.CompanyFactories;
using Ledger.Shared.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.Companies.Tests.Factories
{
    [TestClass]
    public class CompanyFactoryTests
    {

        [TestMethod]
        public void ShouldCreateCompany()
        {
            CompanyFactory factory = new CompanyFactory();

            string name = "Fabrikam";
            string description = "Fictional Microsoft company";
            string cnpj = "5113111310";
            string inscrEstadual = "15115511331";
            string email = "fabrikam@fabrikam.com";
            string ownerName = "Lucas Pereira";
            DateTime ownerBirthDate = DateTime.Now.AddYears(-20);
            string cpf = "81.153.856-99";
            Guid tenantId = Guid.NewGuid();

            Company company = factory.CreateCompany(name, description, email, cnpj, inscrEstadual, ownerName, ownerBirthDate, cpf, tenantId);

            Assert.IsNotNull(company);
            Assert.AreEqual("Fabrikam", company.Name);
            Assert.AreEqual("Fictional Microsoft company", company.Description);
            Assert.AreEqual("81.153.856-99", company.Owner.Cpf.Number);
        }

        [TestMethod]
        public void ShouldCreateCompanyUsingExistingId()
        {
            Guid EXISTING_ID = new Guid("89707872-eab3-4f04-bfe8-7dc20f5d3b9c");

            CompanyFactory factory = new CompanyFactory();

            string name = "Fabrikam";
            string description = "Fictional Microsoft company";
            string cnpj = "5113111310";
            string inscrEstadual = "15115511331";
            string email = "fabrikam@fabrikam.com";
            string ownerName = "Lucas Pereira";
            DateTime ownerBirthDate = DateTime.Now.AddYears(-20);
            string cpf = "81.153.856-99";

            Company company = factory.CreateCompany(name, description, email, cnpj, inscrEstadual, ownerName, ownerBirthDate, cpf, Guid.NewGuid(), EXISTING_ID);

            Assert.IsNotNull(company);
            Assert.AreEqual(new Guid("89707872-eab3-4f04-bfe8-7dc20f5d3b9c"), company.Id);
        }
    }
}
