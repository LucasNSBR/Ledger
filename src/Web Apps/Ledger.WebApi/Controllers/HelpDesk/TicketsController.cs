using Ledger.HelpDesk.Application.AppServices.TicketAppServices;
using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.HelpDesk.Domain.Commands.TicketCommands;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Ledger.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/tickets")]
    //[Authorize(Policy = "ConfirmedAccount")]
    public class TicketsController : BaseController
    {
        private readonly ITicketApplicationService _ticketApplicationService;

        public TicketsController(ITicketApplicationService ticketApplicationService, IDomainNotificationHandler domainNotificationHandler) 
                                                                                                                                                                                : base(domainNotificationHandler)
        {
            _ticketApplicationService = ticketApplicationService;
        }

        [HttpGet]
        [Route("")]
        //[Authorize(Policy = "SupportAccount")]
        public IActionResult GetAllTickets()
        {
            IQueryable<Ticket> tickets = _ticketApplicationService.GetAllTickets();

            return CreateResponse(tickets);
        }

        [HttpGet]
        [Route("user/{id:guid}")]
        public IActionResult GetByUser(Guid id)
        {
            IQueryable<Ticket> tickets = _ticketApplicationService.GetByUserId(id);

            return CreateResponse(tickets);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            Ticket ticket = _ticketApplicationService.GetById(id);

            return CreateResponse(ticket);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Register([FromBody]RegisterTicketCommand command)
        {
            _ticketApplicationService.Register(command);

            return CreateResponse();
        }

        [HttpPut]
        [Route("{id:guid}/attach-picture")]
        public IActionResult AttachIssuePicture(Guid id, [FromBody]AttachIssuePictureCommand command)
        {
            command.TicketId = id;

            _ticketApplicationService.AttachIssuePicture(command);

            return CreateResponse();
        }

        [HttpPut]
        [Route("{id:guid}/assign-support")]
        //[Authorize(Policy = "SupportAccount")]
        public IActionResult AssignSupport(Guid id, [FromBody]AssignSupportCommand command)
        {
            command.TicketId = id;

            _ticketApplicationService.AssignSupport(command);

            return CreateResponse();
        }

        [HttpPut]
        [Route("{id:guid}/add-message")]
        public IActionResult AddMessage(Guid id, [FromBody]AddMessageCommand command)
        {
            command.TicketId = id;

            _ticketApplicationService.AddMessage(command);

            return CreateResponse();
        }

        [HttpPut]
        [Route("{id:guid}/close")]
        public IActionResult Close(Guid id)
        {
            CloseTicketCommand command = new CloseTicketCommand
            {
                TicketId = id
            };

            _ticketApplicationService.Close(command);

            return CreateResponse();
        }        
    }
}