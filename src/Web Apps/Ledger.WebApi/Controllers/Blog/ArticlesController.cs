using Ledger.Blog.Application.AppServices.ArticleAppServices;
using Ledger.Blog.Domain.Aggregates.ArticleAggregate;
using Ledger.Blog.Domain.Commands.ArticleCommands;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Ledger.WebApi.Controllers.Blog
{
    [Produces("application/json")]
    [Route("api/articles")]
    public class ArticlesController : BaseController
    {
        private readonly IArticleApplicationService _articleAppService;

        public ArticlesController(IArticleApplicationService articleAppService, IDomainNotificationHandler domainNotificationHandler) : base(domainNotificationHandler)
        {
            _articleAppService = articleAppService;
        }

        [HttpGet]
        [Route("")]
        [ResponseCache(Duration = 30)]
        public IActionResult GetAllArticles()
        {
            IQueryable<Article> articles = _articleAppService.GetAllArticles();

            return CreateResponse(articles);
        }

        [HttpGet]
        [Route("category/{id:guid}")]
        [ResponseCache(Duration = 30)]
        public IActionResult GetByCategory(Guid id)
        {
            IQueryable<Article> articles = _articleAppService.GetByCategory(id);

            return CreateResponse(articles);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ResponseCache(Duration = 15)]
        public IActionResult GetById(Guid id)
        {
            Article article = _articleAppService.GetById(id);

            return CreateResponse(article);
        }

        [HttpPost]
        [Route("")]
        //[Authorize(Policy = "AdminAccount")]
        public IActionResult Register([FromBody]RegisterArticleCommand command)
        {
            _articleAppService.Register(command);

            return CreateResponse();
        }

        [HttpPut]
        [Route("{id:guid}")]
        //[Authorize(Policy = "AdminAccount")]
        public IActionResult Update(Guid id, [FromBody]UpdateArticleCommand command)
        {
            command.ArticleId = id;

            _articleAppService.Update(command);

            return CreateResponse();
        }

        [HttpPut]
        [Route("{id:guid}/add-comment")]
        [Authorize(Policy = "ActivatedAccount")]
        public IActionResult AddComment(Guid id, [FromBody]AddArticleCommentCommand command)
        {
            command.ArticleId = id;

            _articleAppService.AddComment(command);

            return CreateResponse();
        }

        [HttpPut]
        [Route("{id:guid}/remove-comment")]
        [Authorize(Policy = "ActivatedAccount")]
        public IActionResult Remove(Guid id, [FromBody]RemoveArticleCommentCommand command)
        {
            command.ArticleId = id;

            _articleAppService.RemoveComment(command);

            return CreateResponse();
        }

        [HttpDelete]
        [Route("{id:guid}")]        
        //[Authorize(Policy = "AdminAccount")]
        public IActionResult Remove(Guid id)
        {
            RemoveArticleCommand command = new RemoveArticleCommand
            {
                ArticleId = id
            };

            _articleAppService.Remove(command);

            return CreateResponse();
        }
    }
}