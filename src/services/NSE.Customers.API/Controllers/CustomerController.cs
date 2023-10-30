using Microsoft.AspNetCore.Mvc;
using NSE.Core.Mediator;
using NSE.Customers.API.Application.Commands;
using NSE.WebApi.Core.Controllers;

namespace NSE.Customers.API.Controllers
{
    public class CustomerController : MainController
    {
        private readonly IMediatorHandler _mediator;

        public CustomerController(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("customers")]
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.SendCommand(
                new RegisterCustomerCommand(Guid.NewGuid(), "Christian", "chris@chris.com.br", "93174069041"));

            return CustomResponse(result);
        }
    }
}
