using E_Commerce.Application.Product.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ProductController>/GetAllProducts
        [HttpGet("GetAllProducts")]
        [ProducesResponseType(statusCode:StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _mediator.Send(new GetProductByIdQuery()); 
            return Ok(result);
        }

        // GET api/<ProductController>/GetSingleProduct/5
        [HttpGet("GetSingleProduct/{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSingleProduct(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery());
            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
