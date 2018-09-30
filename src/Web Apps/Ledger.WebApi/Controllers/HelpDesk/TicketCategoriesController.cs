using Ledger.HelpDesk.Application.AppServices.TicketCategoryAppServices;
using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Commands.TicketCategoryCommands;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Ledger.WebApi.Controllers.HelpDesk
{
    [Produces("application/json")]
    [Route("api/ticket-categories")]
    public class TicketCategoriesController : BaseController
    {
        private readonly ITicketCategoryApplicationService _ticketCategoryApplicationService;

        public TicketCategoriesController(ITicketCategoryApplicationService ticketCategoryApplicationService, IDomainNotificationHandler domainNotificationHandler)
                                                                                                                                    : base(domainNotificationHandler)
        {
            _ticketCategoryApplicationService = ticketCategoryApplicationService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetTicketCategories()
        {
            IQueryable<TicketCategory> ticketCategories = _ticketCategoryApplicationService.GetAllCategories();

            return CreateResponse(ticketCategories);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetTicketCategoriesById(Guid id)
        {
            TicketCategory ticketCategory = _ticketCategoryApplicationService.GetById(id);

            return CreateResponse(ticketCategory);
        }

        [HttpPost]
        [Route("")]
        //[Authorize(Roles = "SupportAccount,AdminAccount")]
        public IActionResult RegisterTicketCategory([FromBody]RegisterTicketCategoryCommand command)
        {
            _ticketCategoryApplicationService.Register(command);

            return CreateResponse();
        }

        [HttpPut]
        [Route("{id:guid}")]
        //[Authorize(Roles = "SupportAccount,AdminAccount")]
        public IActionResult UpdateTicketCategory(Guid id, [FromBody]UpdateTicketCategoryCommand command)
        {
            command.CategoryId = id;

            _ticketCategoryApplicationService.Update(command);

            return CreateResponse();
        }
    }
}