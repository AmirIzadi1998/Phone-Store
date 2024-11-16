using Application.CQRS.PhoneProductCQRS.Command;
using Application.CQRS.PhoneProductCQRS.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Phone_Store.Controllers
{
    [Route("api/ProductCQRS")]
    [ApiController]
    public class ProductCQRSController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductCQRSController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Insert(SaveCommand command)
        {
            var product = await _mediator.Send(command);
            return Ok(product);
        }

        [HttpGet("GetId")]
        public async Task<IActionResult> GetById([FromQuery]GetQuery getQuery)
        {
            var product = await _mediator.Send(getQuery);
            return Ok(product);
        }
    }
}
