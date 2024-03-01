using E_Commerce.Application.RootCategory.AddRootCategory;
using E_Commerce.Application.RootCategory.GetAllRootCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RootCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RootCategoryController( IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<RootCategoryController>
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetRootCategories()
        {
            var rootCategories = await _mediator.Send(new GetAllRootCategoriesQuery());
            return Ok(rootCategories.Value);
        }

        // GET api/<RootCategoryController>/5
        [HttpGet("GetSingleCategory/{id}")]
        public string GetSingleRootCategory(int id)
        {
            return "value";
        }

        // POST api/<RootCategoryController>
        [HttpPost("AddNewRootCategory")]
        public async Task<IActionResult> AddNewRootCategory([FromBody] string name)
        {
            var rootCategory = await _mediator.Send(new AddRootCategoryCommand(name));
            return  Ok(rootCategory.Value);
        }

        // PUT api/<RootCategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RootCategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
