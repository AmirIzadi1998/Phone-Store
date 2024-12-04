using Application.CQRS.PhoneProductCQRS.Command;
using Application.CQRS.PhoneProductCQRS.Query;
using Core.Context;
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

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert(SaveCommand command)
        {
            var product = await _mediator.Send(command);
            return Ok(product);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllQuery getAllQuery)
        {
            var product = await _mediator.Send(getAllQuery);
            return Ok(product);
        }

        [HttpGet("GetId")]
        public async Task<IActionResult> GetById([FromQuery] GetQuery getQuery)
        {
            var product = await _mediator.Send(getQuery);
            return Ok(product);
        }
        [HttpGet("GetName")]
        public async Task<IActionResult> GetName([FromQuery] GetByName getByName)
        {
            var product = await _mediator.Send(getByName);
            return Ok(product);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(DeleteCommand deleteCommand)
        {
            var response = await _mediator.Send(deleteCommand);
            return Ok(response);
        }

        [HttpPut("Update")]

        public async Task<IActionResult> Update(UpdateCommand updateCommand)
        {
            var response = await _mediator.Send(updateCommand);
            return Ok(response);
        }
    }
}