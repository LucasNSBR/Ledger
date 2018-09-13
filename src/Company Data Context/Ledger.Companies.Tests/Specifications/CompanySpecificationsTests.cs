using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Companies.Domain.Specifications.CompanySpecifications;
using Ledger.Shared.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.Companies.Tests.Specifications
{
    [TestClass]
    public class CompanySpecificationsTests
    {
        Guid companyId = new Guid("354f3d5b-52e9-4e71-917f-c1a6d977c5a1");
        string cnpj = "59.009.518/0001-41";

        public Company company;

        public CompanySpecificationsTests()
        {
            company = new Company(companyId,
               "Fabrikam", "Fabrikam Corporation", new EmailAddress("admin@fabrikam.com"), new Cnpj(cnpj), new InscricaoEstadual("001.115-550"),
               new Owner("Lucas Pereira Campos", DateTime.Now.AddYears(-20), new Cpf("981.153.856-99")));
        }

        [TestMethod]
        public void ShouldMatchUsingIdSpecification()
        {
            CompanyIdSpecification specification = new CompanyIdSpecification(companyId);
            Assert.IsTrue(specification.IsSatisfiedBy(company));
        }

        [TestMethod]
        public void ShouldMatchUsingCnpjSpecification()
        {
            CompanyCnpjSpecification specification = new CompanyCnpjSpecification(cnpj);
            Assert.IsTrue(specification.IsSatisfiedBy(company));
        }
    }
}
