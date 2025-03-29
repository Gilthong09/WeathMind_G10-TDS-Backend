using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.CategoryV;

namespace WealthMind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Category")]
    public class CategoryController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }


        [HttpPost("add")]
        [SwaggerOperation(
            Summary = "Registers a category.",
            Description = "Recieves the necessary parameters for registering a category."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SaveCategoryViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(SaveCategoryViewModel dto)
        {

            var results = await _categoryService.Add(dto);
            if (results.HasError)
            {
                return BadRequest(results);
            }
            return Ok(results);

        }

        [HttpGet("get")]
        [SwaggerOperation(
            Summary = "Gets a category.",
            Description = "Recieves the necessary parameters for getting a category."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveCategoryViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string id)
        {
            var results = await _categoryService.GetByIdSaveViewModel(id);
            if (results.HasError)
            {
                return BadRequest(results);
            }
            return Ok(results);
        }


        [HttpGet("getall")]
        [SwaggerOperation(
           Summary = "Gets all categories",
           Description = "Recieves the necessary parameters for getting all categories."
       )]
        [Consumes(MediaTypeNames.Application.Json)]
        // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SaveCashViewModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var results = await _categoryService.GetAllViewModel();
            if (results.Any())
            {
                return Ok(results);
            }

            return NotFound();
        }

        [HttpPut("update")]
        [SwaggerOperation(
            Summary = "Updates a category.",
            Description = "Recieves the necessary parameters for updating a category."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveCategoryViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(CategoryViewModel dto, string id)
        {
            var request = _mapper.Map<SaveCategoryViewModel>(dto);

            try
            {
                await _categoryService.Update(request, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("delete")]
        [SwaggerOperation(
            Summary = "Deletes a category.",
            Description = "Recieves the necessary parameters for deleting a category."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _categoryService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
