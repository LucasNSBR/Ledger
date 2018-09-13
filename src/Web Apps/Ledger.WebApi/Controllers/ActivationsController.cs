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

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            Activation activation = _activationAppService.GetById(id);

            return CreateResponse(activation);
        }

        [HttpPost]
        public IActionResult Register([FromBody]RegisterActivationCommand command)
        {
            _activationAppService.RegisterActivation(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("{id:guid}/attach")]
        public IActionResult AttachDocuments([FromBody]AttachCompanyDocumentsCommand command)
        {
            _activationAppService.AttachCompanyDocuments(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("{id:guid}/accept")]
        //[Authorize(Policy = "AdminAccount")]
        public IActionResult Accept(Guid id)
        {
            AcceptActivationCommand command = new AcceptActivationCommand
            {
                ActivationId = id
            };

            _activationAppService.AcceptActivation(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("{id:guid}/reject")]
        [Authorize(Policy = "AdminAccount")]
        public IActionResult Reject(Guid id)
        {
            RejectActivationCommand command = new RejectActivationCommand
            {
                ActivationId = id
            };

            _activationAppService.RejectActivation(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("{id:guid}/reset")]
        [Authorize(Policy = "AdminAccount")]
        public IActionResult Reset(Guid id)
        {
            ResetActivationCommand command = new ResetActivationCommand
            {
                ActivationId = id
            };

            _activationAppService.ResetActivation(command);

            return CreateResponse();
        }
    }
}
