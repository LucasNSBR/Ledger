using Ledger.Blog.Domain.Aggregates.ArticleAggregate;
using Ledger.Blog.Domain.Specifications.ArticleSpecifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ledger.Blog.Tests.Specifications
{
    [TestClass]
    public class ArticleSpecificationTests
    {
        [TestMethod]
        public void ShouldMatchUsingIdSpecification()
        {
            Guid articleId = Guid.NewGuid();

            Article article = new Article(articleId, "ola-mundo", "Olá, mundo!", "Artigo de testes", Guid.NewGuid(), Guid.NewGuid());
            ArticleIdSpecification specification = new ArticleIdSpecification(articleId);

            Assert.IsTrue(specification.IsSatisfiedBy(article));
        }

        [TestMethod]
        public void ShouldMatchUsingCategoryIdSpecification()
        {
            Guid articleCategoryId = Guid.NewGuid();
            Guid articleId = Guid.NewGuid();

            Article article = new Article(articleId, "ola-mundo", "Olá, mundo!", "Artigo de testes", articleCategoryId, Guid.NewGuid());
            ArticleCategoryIdSpecification specification = new ArticleCategoryIdSpecification(articleCategoryId);

            Assert.IsTrue(specification.IsSatisfiedBy(article));
        }
    }
}
