using E_Commerce.Application.RootCategory.AddRootCategory;
using E_Commerce.Application.RootCategory.ChangeRootCategoryName;
using E_Commerce.Application.RootCategory.GetAllRootCategories;
using E_Commerce.Application.RootCategory.GetSingleRootCategoryQuery;
using E_Commerce.Application.RootCategory.RemoveRootCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Api.Controller
{
    [Authorize]
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
        public async Task<IActionResult> GetRootAllCategories()
        {
            var rootCategories = await _mediator.Send(new GetAllRootCategoriesQuery());
            return Ok(rootCategories.Value);
        }

        // GET api/<RootCategoryController>/5
        [HttpGet("GetSingleCategory/{id}")]
        public async Task<IActionResult> GetSingleRootCategory(Guid id)
        {
            var result = await _mediator.Send(new GetSingleRootCategoryQuery(id));
            return Ok(result);
        }

        // POST api/<RootCategoryController>
        [HttpPost("AddNewRootCategory")]
        public async Task<IActionResult> AddNewRootCategory([FromBody] string name)
        {
            var rootCategory = await _mediator.Send(new AddRootCategoryCommand(name));
            return  Ok(rootCategory.Value);
        }

        // PUT api/<RootCategoryController>/5
        [HttpPut("ChangeName/{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] string value)
        {
            var result = await _mediator.Send(new ChangeRootCategoryNameCommand(id,value));
            return Ok(result);

        }

        // DELETE api/<RootCategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new RemoveRootCategoryCommand(id));
            return Ok(result);
        }
    }
}
