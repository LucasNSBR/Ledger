using Ledger.Companies.Application.AppServices.CompanyAppServices;
using Ledger.Companies.Domain.Aggregates.CompanyAggregate;
using Ledger.Companies.Domain.Commands;
using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ledger.WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/companies")]
    public class CompaniesController : BaseController
    {
        private readonly ICompanyApplicationService _companyApplicationService;

        public CompaniesController(ICompanyApplicationService companyApplicationService, IDomainNotificationHandler domainNotificationHandler) : base(domainNotificationHandler)
        {
            _companyApplicationService = companyApplicationService;
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            Company company = _companyApplicationService.GetById(id);

            return CreateResponse(company);
        }
        
        [HttpPost]
        public IActionResult Register(RegisterCompanyCommand command)
        {
            _companyApplicationService.Register(command);

            return CreateResponse();
        }

        [HttpPut]
        public IActionResult Update(UpdateCompanyCommand command)
        {
            _companyApplicationService.Update(command);

            return CreateResponse();
        }
        
        [HttpPost]
        [Route("changeaddress")]
        public IActionResult ChangeAddress(ChangeCompanyAddressCommand command)
        {
            _companyApplicationService.ChangeAddress(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("changephone")]
        public IActionResult ChangePhone(ChangeCompanyPhoneCommand command)
        {
            _companyApplicationService.ChangePhone(command);

            return CreateResponse();
        }
    }
}