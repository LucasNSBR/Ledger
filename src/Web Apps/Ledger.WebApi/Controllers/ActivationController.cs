using Ledger.Activations.Application.AppServices.ActivationAppServices;
using Ledger.Activations.Domain.Aggregates.ActivationAggregate;
using Ledger.Activations.Domain.Commands;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ledger.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/activations")]
    public class ActivationController : BaseController
    {
        private readonly IActivationApplicationService _activationAppService;

        public ActivationController(IDomainNotificationHandler domainNotificationHandler, IActivationApplicationService activationAppServiceService) 
                                                                                                                : base(domainNotificationHandler)
        {
            _activationAppService = activationAppServiceService;
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
        public IActionResult Accept([FromBody]AcceptActivationCommand command)
        {
            _activationAppService.AcceptActivation(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("{id:guid}/reject")]
        public IActionResult Reject([FromBody]RejectActivationCommand command)
        {
            _activationAppService.RejectActivation(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("{id:guid}/reset")]
        public IActionResult Reset([FromBody]ResetActivationCommand command)
        {
            _activationAppService.ResetActivation(command);

            return CreateResponse();
        }
    }
}
