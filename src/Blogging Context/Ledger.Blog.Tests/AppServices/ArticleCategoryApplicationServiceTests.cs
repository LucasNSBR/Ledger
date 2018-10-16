using Ledger.Blog.Application.AppServices.ArticleCategoryAppServices;
using Ledger.Blog.Domain.Aggregates.CategoryAggregate;
using Ledger.Blog.Domain.Commands.ArticleCategoryCommands;
using Ledger.Blog.Tests.Mocks;
using Ledger.Shared.Notifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Ledger.Blog.Tests.AppServices
{
    [TestClass]
    public class ArticleCategoryApplicationServiceTests
    {
        ArticleCategoryApplicationService applicationService;
        FakeUnitOfWork fakeUnitOfWork;
        FakeServiceBus fakeServiceBus;
        FakeDomainBus fakeDomainBus;
        FakeArticleCategoryRepository repository;
        DomainNotificationHandler domainNotificationHandler;

        public ArticleCategoryApplicationServiceTests()
        {
            fakeUnitOfWork = new FakeUnitOfWork();
            fakeServiceBus = new FakeServiceBus();
            fakeDomainBus = new FakeDomainBus();
            domainNotificationHandler = new DomainNotificationHandler();
            repository = new FakeArticleCategoryRepository();

            applicationService = new ArticleCategoryApplicationService(repository, domainNotificationHandler, fakeUnitOfWork, fakeServiceBus, fakeDomainBus);
        }

        public void PopulateRepository()
        {
            repository.Register(new ArticleCategory(new Guid("9008f2ce-3700-42ca-a514-48d0fa37c2f6"), "Javascript"));
            repository.Register(new ArticleCategory(new Guid("f9160a4c-d69e-4428-ae44-89ba599919b9"), "C#"));
        }

        [TestMethod]
        public void ShouldGetAllArticleCategories()
        {
            PopulateRepository();

            Assert.AreEqual(2, applicationService.GetAllCategories().Count());
        }

        [TestMethod]
        public void ShouldArticleCategoryGetById()
        {
            PopulateRepository();

            ArticleCategory category = applicationService.GetById(new Guid("9008f2ce-3700-42ca-a514-48d0fa37c2f6"));

            Assert.AreEqual("Javascript", category.Name);
        }

        [TestMethod]
        public void ShouldRegisterArticleCategory()
        {
            RegisterArticleCategoryCommand command = new RegisterArticleCategoryCommand
            {
                Name = "Novos tópicos"
            };

            applicationService.Register(command);

            Assert.AreEqual(1, applicationService.GetAllCategories().Count());
        }

        [TestMethod]
        public void ShouldUpdateArticleCategory()
        {
            PopulateRepository();

            UpdateArticleCategoryCommand command = new UpdateArticleCategoryCommand
            {
                CategoryId = new Guid("9008f2ce-3700-42ca-a514-48d0fa37c2f6"),
                Name = "RabbitMQ e AMPQ"
            };

            applicationService.Update(command);

            Assert.AreEqual("RabbitMQ e AMPQ", applicationService.GetById(new Guid("9008f2ce-3700-42ca-a514-48d0fa37c2f6")).Name);
        }

        [TestMethod]
        public void ShouldRemoveArticleCategory()
        {
            PopulateRepository();

            RemoveArticleCategoryCommand command = new RemoveArticleCategoryCommand
            {
                CategoryId = new Guid("9008f2ce-3700-42ca-a514-48d0fa37c2f6"),
            };

            applicationService.Remove(command);

            Assert.AreEqual(1, applicationService.GetAllCategories().Count());
        }
    }
}
