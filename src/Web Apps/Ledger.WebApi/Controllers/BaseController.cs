using Ledger.Shared.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ledger.WebApi.Controllers
{
    public class BaseController : Controller
    {
        private readonly IDomainNotificationHandler _domainNotificationHandler;

        public BaseController(IDomainNotificationHandler domainNotificationHandler)
        {
            _domainNotificationHandler = domainNotificationHandler;
        }

        public IActionResult CreateResponse(object result = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (_domainNotificationHandler.HasNotifications())
            {
                return BadRequest(new
                {
                    success = false,
                    result = "Ocorreu um erro ao retornar os resultados.",
                    errors = _domainNotificationHandler
                            .GetNotifications()
                });
            }

            return StatusCode((int)statusCode, new
            {
                success = true,
                result = "A operação foi concluída com sucesso.",
                data = result
            });
        }

        public IActionResult CreateErrorResponse(object message = null)
        {
            return BadRequest(new
            {
                success = false,
                result = message ?? "Ocorreu um erro ao retornar os resultados.",
            });
        }
    }
}