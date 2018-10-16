using Ledger.Blog.Application.AppServices.ArticleAppServices;
using Ledger.Blog.Domain.Aggregates.ArticleAggregate;
using Ledger.Blog.Domain.Aggregates.CategoryAggregate;
using Ledger.Blog.Domain.Commands.ArticleCommands;
using Ledger.Blog.Tests.Mocks;
using Ledger.Shared.Notifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Ledger.Blog.Tests.AppServices
{
    [TestClass]
    public class ArticleApplicationServiceTests
    {
        Guid userId = new Guid("5227c760-f6f0-4e72-b3fa-19059c58d8e3");

        ArticleApplicationService applicationService;
        FakeArticleCategoryRepository categoryRepository;
        FakeUnitOfWork fakeUnitOfWork;
        FakeServiceBus fakeServiceBus;
        FakeIdentityResolver identityResolver;
        FakeDomainBus fakeDomainBus;
        FakeArticleRepository repository;
        DomainNotificationHandler domainNotificationHandler;

        public ArticleApplicationServiceTests()
        {
            fakeUnitOfWork = new FakeUnitOfWork();
            fakeServiceBus = new FakeServiceBus();
            domainNotificationHandler = new DomainNotificationHandler();
            categoryRepository = new FakeArticleCategoryRepository();
            repository = new FakeArticleRepository();
            fakeDomainBus = new FakeDomainBus();
            identityResolver = new FakeIdentityResolver();

            applicationService = new ArticleApplicationService(repository, categoryRepository, identityResolver, domainNotificationHandler, fakeUnitOfWork, fakeServiceBus, fakeDomainBus);
        }

        public void PopulateRepository()
        {
            //Populate categories too
            categoryRepository.Register(new ArticleCategory(new Guid("9008f2ce-3700-42ca-a514-48d0fa37c2f6"), "Javascript"));
            categoryRepository.Register(new ArticleCategory(new Guid("f9160a4c-d69e-4428-ae44-89ba599919b9"), "C#"));

            repository.Register(new Article(new Guid("4aeabf09-da78-4fcd-bb73-667605871a5e"), "ola-mundo", "Olá, mundo!", "Artigo de testes", new Guid("9008f2ce-3700-42ca-a514-48d0fa37c2f6"), new Guid("5227c760-f6f0-4e72-b3fa-19059c58d8e3")));
            repository.Register(new Article(new Guid("66aecb63-30db-4c7c-8048-55e4e808a974"), "ola-production", "Olá, produção!", "Artigo de production", new Guid("f9160a4c-d69e-4428-ae44-89ba599919b9"), new Guid("5227c760-f6f0-4e72-b3fa-19059c58d8e3")));
        }

        [TestMethod]
        public void ShouldGetAllArticles()
        {
            PopulateRepository();

            Assert.AreEqual(2, applicationService.GetAllArticles().Count());
        }

        [TestMethod]
        public void ShouldGetArticlesByCategory()
        {
            PopulateRepository();

            Guid categoryId = new Guid("f9160a4c-d69e-4428-ae44-89ba599919b9");

            Assert.AreEqual(1, applicationService.GetByCategory(categoryId).Count());
        }

        [TestMethod]
        public void ShouldGetArticleById()
        {
            PopulateRepository();

            Article article = applicationService.GetById(new Guid("4aeabf09-da78-4fcd-bb73-667605871a5e"));

            Assert.AreEqual("Olá, mundo!", article.Title);
        }

        [TestMethod]
        public void ShouldRegisterArticle()
        {
            PopulateRepository();

            RegisterArticleCommand command = new RegisterArticleCommand
            {
                Body = "Olá, mundo! Esse é um artigo com mais de 50 caracteres para passar no teste.",
                Slug = "artigo-do-ola-mundo",
                Title = "Título do artigo aqui",
                CategoryId = new Guid("9008f2ce-3700-42ca-a514-48d0fa37c2f6")
            };

            applicationService.Register(command);

            Assert.AreEqual(3, applicationService.GetAllArticles().Count());
        }

        [TestMethod]
        public void ShouldUpdateArticle()
        {
            PopulateRepository();

            UpdateArticleCommand command = new UpdateArticleCommand
            {
                Body = "Lorem ipsun, Lorem ipsun, Lorem ipsun, Lorem ipsun, Lorem ipsun",
                Slug = "artigo-do-ola-mundo",
                Title = "Título do artigo aqui",
                CategoryId = new Guid("9008f2ce-3700-42ca-a514-48d0fa37c2f6"),
                ArticleId = new Guid("4aeabf09-da78-4fcd-bb73-667605871a5e")
            };

            applicationService.Update(command);

            Article article = applicationService.GetById(new Guid("4aeabf09-da78-4fcd-bb73-667605871a5e"));

            Assert.AreEqual("Lorem ipsun, Lorem ipsun, Lorem ipsun, Lorem ipsun, Lorem ipsun", article.Body);
        }

        [TestMethod]
        public void ShouldRemoveArticle()
        {
            PopulateRepository();

            RemoveArticleCommand command = new RemoveArticleCommand
            {
                ArticleId = new Guid("4aeabf09-da78-4fcd-bb73-667605871a5e")
            };

            applicationService.Remove(command);

            Assert.AreEqual(1, applicationService.GetAllArticles().Count());
        }

        [TestMethod]
        public void ShouldSetArticleActive()
        {
            PopulateRepository();

            SetActiveArticleCommand command = new SetActiveArticleCommand
            {
                ArticleId = new Guid("4aeabf09-da78-4fcd-bb73-667605871a5e")
            };

            applicationService.SetActive(command);

            Article article = applicationService.GetById(new Guid("4aeabf09-da78-4fcd-bb73-667605871a5e"));

            Assert.IsTrue(article.Active);
        }

        [TestMethod]
        public void ShouldSetArticleInactive()
        {
            PopulateRepository();

            SetInactiveArticleCommand command = new SetInactiveArticleCommand
            {
                ArticleId = new Guid("4aeabf09-da78-4fcd-bb73-667605871a5e")
            };

            applicationService.SetInactive(command);

            Article article = applicationService.GetById(new Guid("4aeabf09-da78-4fcd-bb73-667605871a5e"));

            Assert.IsFalse(article.Active);
        }

        [TestMethod]
        public void ShouldAddArticleComment()
        {
            PopulateRepository();

            AddArticleCommentCommand command = new AddArticleCommentCommand
            {
                ArticleId = new Guid("4aeabf09-da78-4fcd-bb73-667605871a5e"),
                Body = "hELLO IN COmMeNtIng hERe!!"
            };

            applicationService.AddComment(command);

            Article article = applicationService.GetById(new Guid("4aeabf09-da78-4fcd-bb73-667605871a5e"));

            Assert.AreEqual(1, article.Comments.Count());
        }

        [TestMethod]
        public void ShouldAddAndRemoveArticleComment()
        {
            PopulateRepository();

            #region Add
            AddArticleCommentCommand addCommand = new AddArticleCommentCommand
            {
                ArticleId = new Guid("4aeabf09-da78-4fcd-bb73-667605871a5e"),
                Body = "hELLO Im COmMeNtIng hERe!!"
            };

            applicationService.AddComment(addCommand);

            Article article = applicationService.GetById(new Guid("4aeabf09-da78-4fcd-bb73-667605871a5e"));

            Assert.AreEqual(1, article.Comments.Count());
            #endregion

            #region Remove
            RemoveArticleCommentCommand removeCommand = new RemoveArticleCommentCommand
            {
                ArticleId = new Guid("4aeabf09-da78-4fcd-bb73-667605871a5e"),
                CommentId = article.Comments.First().Id
            };

            applicationService.RemoveComment(removeCommand);

            Assert.AreEqual(1, article.Comments.Count());
            #endregion
        }

        [TestMethod]
        public void ShouldFailAddAndRemoveArticleCommentDifferentUser()
        {
            PopulateRepository();

            #region Add
            AddArticleCommentCommand addCommand = new AddArticleCommentCommand
            {
                ArticleId = new Guid("4aeabf09-da78-4fcd-bb73-667605871a5e"),
                Body = "hELLO IN COmMeNtIng hERe!!"
            };

            applicationService.AddComment(addCommand);

            Article article = applicationService.GetById(new Guid("4aeabf09-da78-4fcd-bb73-667605871a5e"));

            Assert.AreEqual(1, article.Comments.Count());
            #endregion

            //REFRESH USER ID
            identityResolver.RefreshId();
            //NOW IT'S LIKE A DIFFERENT USER

            #region Remove
            RemoveArticleCommentCommand removeCommand = new RemoveArticleCommentCommand
            {
                ArticleId = new Guid("4aeabf09-da78-4fcd-bb73-667605871a5e"),
                CommentId = article.Comments.First().Id
            };

            applicationService.RemoveComment(removeCommand);

            Assert.AreEqual("Erro na remoção", domainNotificationHandler.GetNotifications().First().Title);
            #endregion
        }
    }
}