using Ledger.HelpDesk.Application.AppServices.TicketCategoryAppServices;
using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Commands.TicketCategoryCommands;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Ledger.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/helpdesk")]
    public class HelpDeskController : BaseController
    {
        private readonly ITicketCategoryApplicationService _ticketCategoryAppService;

        public HelpDeskController(ITicketCategoryApplicationService ticketCategoryAppService, IDomainNotificationHandler domainNotificationHandler) : base(domainNotificationHandler)
        {
            _ticketCategoryAppService = ticketCategoryAppService;
        }

        [HttpGet("categories")]
        public IActionResult Get()
        {
            IQueryable<TicketCategory> ticketCategory = _ticketCategoryAppService.GetAllCategories();

            return CreateErrorResponse(ticketCategory);
        }

        [HttpGet("categories/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            TicketCategory ticketCategory = _ticketCategoryAppService.GetById(id);

            return CreateResponse(ticketCategory);
        }

        [HttpPost]
        [Route("categories")]
        [Authorize(Policy = "SupportAccount")]
        public IActionResult RegisterTicketCategory([FromBody]RegisterTicketCategoryCommand command)
        {
            _ticketCategoryAppService.Register(command);

            return CreateResponse();
        }

        [HttpPut]
        [Route("categories")]
        [Authorize(Policy = "SupportAccount")]
        public IActionResult UpdateTicketCategory([FromBody]UpdateTicketCategoryCommand command)
        {
            _ticketCategoryAppService.Update(command);

            return CreateResponse();
        }

    }
}