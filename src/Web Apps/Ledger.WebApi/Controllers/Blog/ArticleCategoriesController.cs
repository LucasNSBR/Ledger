using Ledger.Blog.Application.AppServices.ArticleCategoryAppServices;
using Ledger.Blog.Domain.Aggregates.CategoryAggregate;
using Ledger.Blog.Domain.Commands.ArticleCategoryCommands;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Ledger.WebApi.Controllers.Blog
{
    [Produces("application/json")]
    [Route("api/article-categories")]
    public class ArticleCategoriesController : BaseController
    {
        private readonly IArticleCategoryApplicationService _categoryAppService;

        public ArticleCategoriesController(IArticleCategoryApplicationService categoryAppService, IDomainNotificationHandler domainNotificationHandler) : base(domainNotificationHandler)
        {
            _categoryAppService = categoryAppService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllCategories()
        {
            IQueryable<ArticleCategory> categories = _categoryAppService.GetAllCategories();

            return CreateResponse(categories);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            ArticleCategory category = _categoryAppService.GetById(id);

            return CreateResponse(category);
        }

        [HttpPost]
        [Route("")]
        //[Authorize(Policy = "AdminAccount")]
        public IActionResult Register([FromBody]RegisterArticleCategoryCommand command)
        {
            _categoryAppService.Register(command);

            return CreateResponse();
        }

        [HttpPut]
        [Route("{id:guid}")]
        //[Authorize(Policy = "AdminAccount")]
        public IActionResult Update(Guid id, [FromBody]UpdateArticleCategoryCommand command)
        {
            command.CategoryId = id;

            _categoryAppService.Update(command);

            return CreateResponse();
        }


        [HttpDelete]
        [Route("{id:guid}")]
        //[Authorize(Policy = "AdminAccount")]
        public IActionResult Update(Guid id, [FromBody]RemoveArticleCategoryCommand command)
        {
            command.CategoryId = id;

            _categoryAppService.Remove(command);

            return CreateResponse();
        }
    }
}