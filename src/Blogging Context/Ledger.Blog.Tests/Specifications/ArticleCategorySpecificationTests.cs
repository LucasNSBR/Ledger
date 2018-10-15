using Ledger.Blog.Domain.Aggregates.CategoryAggregate;
using Ledger.Blog.Domain.Specifications.ArticleCategorySpecifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.Blog.Tests.Specifications
{
    [TestClass]
    public class ArticleCategorySpecificationTests
    {
        [TestMethod]
        public void ShouldMatchUsingArticleCategoryIdSpecification()
        {
            Guid id = Guid.NewGuid();

            ArticleCategory articleCategory = new ArticleCategory(id, "Suporte");
            ArticleCategoryIdSpecification specification = new ArticleCategoryIdSpecification(id);

            Assert.IsTrue(specification.IsSatisfiedBy(articleCategory));
        }
    }
}
