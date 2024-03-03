using E_Commerce.Application.Category.AddCategoryForChild;
using E_Commerce.Application.Category.AddCategoryForRoot;
using E_Commerce.Application.Category.GetAllCategories;
using E_Commerce.Application.Category.GetSingleCategory;
using E_Commerce.Application.Category.RemoveCategory;
using E_Commerce.Application.Category.UpdateCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        // GET: api/<CategoryController>
        [HttpGet("GetAllCategoreis")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery());

            return Ok(result);
        }

        // GET api/<CategoryController>/5
        [HttpGet("GetSingleCategory/{id}")]
        public async Task<IActionResult> GetSingleCategory(Guid id)
        {
            var result = await _mediator.Send(new GetSingleCategoryQuery(id));

            return Ok(result);
        }

        // POST api/<CategoryController>
        [HttpPost("AddCategoryAsRoot")]
        public async Task<IActionResult> AddCategoryAsRoot([FromBody]Guid rootCategoryId, string categoryName)
        {
            var result = await _mediator.Send(new AddCategoryForRootCommand(rootCategoryId,categoryName));

            return Ok(result);
        }

        [HttpPost("AddCategoryAsChild")]
        public async Task<IActionResult> AddCategoryAsChild([FromBody] Guid categoryId, string categoryName)
        {
            var result = await _mediator.Send(new AddCategoryForChildCommand(categoryId, categoryName));

            return Ok(result);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("UpddateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(Guid categoryId, [FromBody] string name)
        {
            var result = await _mediator.Send(new UpdateCategoryCommand(categoryId,name)) ;

            return Ok(result);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new RemoveCategoryCommand(id));

            return Ok(result);
        }
    }

    
}
