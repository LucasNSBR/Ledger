using Ledger.Activations.Application.AppServices.ActivationAppServices;
using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Commands;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ledger.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/activations")]
    //[Authorize(Policy = "ActivatedAccount")]
    public class ActivationsController : BaseController
    {
        private readonly IActivationApplicationService _activationAppService;

        public ActivationsController(IActivationApplicationService activationAppService, IDomainNotificationHandler domainNotificationHandler)
                                                                                                                : base(domainNotificationHandler)
        {
            _activationAppService = activationAppService;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            Activation activation = _activationAppService.GetById(id);

            return CreateResponse(activation);
        }

        [HttpPut]
        [Route("{id:guid}/attach")]
        public IActionResult AttachCompanyDocuments(Guid id, [FromBody]AttachCompanyDocumentsCommand command)
        {
            command.ActivationId = id;

            _activationAppService.AttachCompanyDocuments(command);

            return CreateResponse();
        }

        [HttpPut]
        [Route("{id:guid}/accept")]
        //[Authorize(Policy = "AdminAccount")]
        public IActionResult Accept(Guid id)
        {
            AcceptActivationCommand command = new AcceptActivationCommand
            {
                ActivationId = id
            };

            _activationAppService.Accept(command);

            return CreateResponse();
        }

        [HttpPut]
        [Route("{id:guid}/reject")]
        //[Authorize(Policy = "AdminAccount")]
        public IActionResult Reject(Guid id)
        {
            RejectActivationCommand command = new RejectActivationCommand
            {
                ActivationId = id
            };

            _activationAppService.Reject(command);

            return CreateResponse();
        }

        [HttpPut]
        [Route("{id:guid}/reset")]
        public IActionResult Reset(Guid id)
        {
            ResetActivationCommand command = new ResetActivationCommand
            {
                ActivationId = id
            };

            _activationAppService.Reset(command);

            return CreateResponse();
        }
    }
}
