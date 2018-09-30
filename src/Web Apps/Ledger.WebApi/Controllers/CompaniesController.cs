using Ledger.Companies.Application.AppServices.CompanyAppServices;
using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Companies.Domain.Commands;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ledger.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/companies")]
    //[Authorize(Policy = "ActivatedAccount")]
    public class CompaniesController : BaseController
    {
        private readonly ICompanyApplicationService _companyApplicationService;

        public CompaniesController(ICompanyApplicationService companyApplicationService, IDomainNotificationHandler domainNotificationHandler) : base(domainNotificationHandler)
        {
            _companyApplicationService = companyApplicationService;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            Company company = _companyApplicationService.GetById(id);

            return CreateResponse(company);
        }
        
        [HttpPost]
        [Route("")]
        public IActionResult Register([FromBody]RegisterCompanyCommand command)
        {
            _companyApplicationService.Register(command);

            return CreateResponse();
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody]UpdateCompanyCommand command)
        {
            command.CompanyId = id;

            _companyApplicationService.Update(command);

            return CreateResponse();
        }
        
        [HttpPut]
        [Route("{id:guid}/address")]
        public IActionResult ChangeAddress(Guid id, [FromBody]ChangeCompanyAddressCommand command)
        {
            command.CompanyId = id;

            _companyApplicationService.ChangeAddress(command);

            return CreateResponse();
        }

        [HttpPut]
        [Route("{id:guid}/phone")]
        public IActionResult ChangePhone(Guid id, [FromBody]ChangeCompanyPhoneCommand command)
        {
            command.CompanyId = id;

            _companyApplicationService.ChangePhone(command);

            return CreateResponse();
        }
    }
}