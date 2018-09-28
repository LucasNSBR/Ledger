using Ledger.HelpDesk.Application.AppServices.TicketAppServices;
using Ledger.HelpDesk.Application.AppServices.TicketCategoryAppServices;
using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.HelpDesk.Domain.Commands.TicketCategoryCommands;
using Ledger.HelpDesk.Domain.Commands.TicketCommands;
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
        private readonly ITicketApplicationService _ticketApplicationService;
        private readonly ITicketCategoryApplicationService _ticketCategoryApplicationService;

        public HelpDeskController(ITicketApplicationService ticketApplicationService, ITicketCategoryApplicationService ticketCategoryApplicationService, IDomainNotificationHandler domainNotificationHandler) : base(domainNotificationHandler)
        {
            _ticketApplicationService = ticketApplicationService;
            _ticketCategoryApplicationService = ticketCategoryApplicationService;
        }

        //TODO: Separate categories/tickets controllers

        #region Tickets
        [HttpGet]
        [Route("tickets/user/{userId:guid}")]
        public IActionResult GetTickets(Guid userId)
        {
            IQueryable<Ticket> tickets = _ticketApplicationService.GetByUserId(userId);

            return CreateResponse(tickets);
        }

        [HttpGet]
        [Route("tickets/{id}")]
        public IActionResult GetTicketById(Guid id)
        {
            Ticket ticket = _ticketApplicationService.GetById(id);

            return CreateResponse(ticket);
        }

        [HttpPost]
        [Route("tickets")]
        public IActionResult RegisterTicket(RegisterTicketCommand command)
        {
            _ticketApplicationService.Register(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("tickets/attach-picture")]
        public IActionResult AttachIssuePicture(AttachIssuePictureCommand command)
        {
            _ticketApplicationService.AttachIssuePicture(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("tickets/assign-support")]
        public IActionResult AssignSupport(AssignSupportCommand command)
        {
            _ticketApplicationService.AssignSupport(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("tickets/add-message")]
        public IActionResult AddMessage(AddMessageCommand command)
        {
            _ticketApplicationService.AddMessage(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("tickets/close")]
        public IActionResult CloseTicket(CloseTicketCommand command)
        {
            _ticketApplicationService.Close(command);

            return CreateResponse();
        }
        #endregion

        #region Categories
        [HttpGet("categories")]
        public IActionResult GetTicketCategories()
        {
            IQueryable<TicketCategory> ticketCategories = _ticketCategoryApplicationService.GetAllCategories();

            return CreateResponse(ticketCategories);
        }

        [HttpGet("categories/{id:guid}")]
        public IActionResult GetTicketCategoriesById(Guid id)
        {
            TicketCategory ticketCategory = _ticketCategoryApplicationService.GetById(id);

            return CreateResponse(ticketCategory);
        }

        [HttpPost]
        [Route("categories")]
        //[Authorize(Roles = "SupportAccount,AdminAccount")]
        public IActionResult RegisterTicketCategory([FromBody]RegisterTicketCategoryCommand command)
        {
            _ticketCategoryApplicationService.Register(command);

            return CreateResponse();
        }

        [HttpPut]
        [Route("categories")]
        //[Authorize(Roles = "SupportAccount,AdminAccount")]
        public IActionResult UpdateTicketCategory([FromBody]UpdateTicketCategoryCommand command)
        {
            _ticketCategoryApplicationService.Update(command);

            return CreateResponse();
        }
        #endregion
    }
}