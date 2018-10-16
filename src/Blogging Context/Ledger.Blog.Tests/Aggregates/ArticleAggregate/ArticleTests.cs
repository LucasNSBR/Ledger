using Ledger.Blog.Domain.Aggregates.ArticleAggregate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Ledger.Blog.Tests.Aggregates.ArticleAggregate
{
    [TestClass]
    public class ArticleTests
    {
        Article article;

        public ArticleTests()
        {
            Guid articleId = new Guid("627c9c3d-6c0e-40ec-8c51-af125c807938");
            article = new Article(articleId, "ola-mundo", "Olá, mundo!", "Artigo de testes", Guid.NewGuid(), Guid.NewGuid());
        }
        
        [TestMethod]
        public void ShouldSetArticleInactive()
        {
            article.SetInactive();

            Assert.IsFalse(article.Active);
        }


        [TestMethod]
        public void ShouldFailToSetArticleInactive()
        {
            article.SetInactive();
            article.SetInactive();

            Assert.AreEqual("Já está inativo", article.GetNotifications().First().Title);
        }

        [TestMethod]
        public void ShouldSetArticleActive()
        {
            article.SetInactive();
            article.SetActive();

            Assert.IsTrue(article.Active);
        }

        [TestMethod]
        public void ShouldFailToSetArticleActive()
        {
            article.SetActive();
            
            Assert.AreEqual("Já ativo", article.GetNotifications().First().Title);
        }

        [TestMethod]
        public void ShouldAddComment()
        {
            Comment comment = new Comment(article.Id, Guid.NewGuid(), "First!");
            Comment commentTwo = new Comment(article.Id, Guid.NewGuid(), "Estou comentando nesse artigo para testar.");

            article.AddComment(comment);
            article.AddComment(commentTwo);

            Assert.AreEqual(2, article.Comments.Count);
        }

        [TestMethod]
        public void ShouldRemoveComment()
        {
            Comment comment = new Comment(article.Id, Guid.NewGuid(), "First!");
            article.AddComment(comment);

            Assert.AreEqual(1, article.Comments.Count);
            
            article.RemoveComment(comment);

            Assert.AreEqual(0, article.Comments.Count);
        }

        [TestMethod]
        public void ShouldFailToRemoveComment()
        {
            Comment comment = new Comment(article.Id, Guid.NewGuid(), "First!");
            article.RemoveComment(comment);

            Assert.AreEqual("Erro na remoção", article.GetNotifications().First().Title);
        }
    }
}
