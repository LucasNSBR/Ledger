using Ledger.Blog.Domain.Aggregates.ArticleAggregate;
using Ledger.Blog.Domain.Aggregates.CategoryAggregate;
using Ledger.Blog.Domain.Commands.ArticleCommands;
using Ledger.Blog.Domain.Context;
using Ledger.Blog.Domain.Repositories.ArticleCategoryRepositories;
using Ledger.Blog.Domain.Repositories.ArticleRepositories;
using Ledger.CrossCutting.Data.UnitOfWork;
using Ledger.CrossCutting.Identity.Aggregates.UserAggregate;
using Ledger.CrossCutting.Identity.Services.UserServices.IdentityResolver;
using Ledger.CrossCutting.ServiceBus.Abstractions;
using Ledger.Shared.Notifications;
using System;
using System.Linq;

namespace Ledger.Blog.Application.AppServices.ArticleAppServices
{
    public class ArticleApplicationService : BaseApplicationService, IArticleApplicationService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _categoryRepository;
        private readonly IIdentityResolver _identityResolver;

        public ArticleApplicationService(IArticleRepository articleRepository, IArticleCategoryRepository categoryRepository, IIdentityResolver identityResolver, IDomainNotificationHandler domainNotificationHandler, IUnitOfWork<ILedgerBlogDbAbstraction> unitOfWork, IIntegrationServiceBus integrationBus, IDomainServiceBus domainBus) : base(domainNotificationHandler, unitOfWork, integrationBus, domainBus)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _identityResolver = identityResolver;
        }

        public IQueryable<Article> GetAllArticles()
        {
            return _articleRepository.GetAllArticles();
        }

        public IQueryable<Article> GetByCategory(Guid categoryId)
        {
            return _articleRepository.GetByCategory(categoryId);
        }

        public Article GetById(Guid id)
        {
            return _articleRepository.GetById(id);
        }

        public void Register(RegisterArticleCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            ArticleCategory category = _categoryRepository.GetById(command.CategoryId);
            User user = _identityResolver.GetUser();

            if (NotifyNullCategory(category))
                return;

            Article article = new Article(command.Slug, command.Title, command.Body, command.CategoryId, user.Id);

            _articleRepository.Register(article);

            Commit();
        }

        public void Update(UpdateArticleCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            ArticleCategory category = _categoryRepository.GetById(command.CategoryId);
            Article article = _articleRepository.GetById(command.ArticleId);
            User user = _identityResolver.GetUser();

            if (NotifyNullCategory(category) || NotifyNullArticle(article))
                return;

            article = new Article(command.ArticleId, command.Slug, command.Title, command.Body, command.CategoryId, user.Id);

            _articleRepository.Update(article);

            Commit();
        }

        public void SetActive(SetActiveArticleCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

           Article article = _articleRepository.GetById(command.ArticleId);

            if (NotifyNullArticle(article))
                return;

            article.SetActive();

            _articleRepository.Update(article);

            Commit();
        }

        public void SetInactive(SetInactiveArticleCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Article article = _articleRepository.GetById(command.ArticleId);

            if (NotifyNullArticle(article))
                return;

            article.SetInactive();

            _articleRepository.Update(article);

            Commit();
        }

        public void AddComment(AddArticleCommentCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Article article = _articleRepository.GetById(command.ArticleId);
            User user = _identityResolver.GetUser();

            if (NotifyNullArticle(article))
                return;

            Comment comment = new Comment(article.Id, user.Id, command.Body);

            article.AddComment(comment);
            _articleRepository.Update(article);

            Commit();
        }

        public void RemoveComment(RemoveArticleCommentCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Article article = _articleRepository.GetById(command.ArticleId);
            User user = _identityResolver.GetUser();

            if (NotifyNullArticle(article))
                return;

            Comment comment = article.Comments.FirstOrDefault(c => c.Id == command.CommentId);

            if(comment == null || comment.AuthorId != user.Id)
            {
                AddNotification("Erro na remoção", "Não foi possível remover esse comentário.");
                return;
            }

            _articleRepository.Update(article);

            Commit();
        }

        public void Remove(RemoveArticleCommand command)
        {
            command.Validate();

            if (AddNotifications(command))
                return;

            Article article = _articleRepository.GetById(command.ArticleId);

            if (NotifyNullArticle(article))
                return;

            _articleRepository.Remove(article);

            Commit();
        }
    }
}
