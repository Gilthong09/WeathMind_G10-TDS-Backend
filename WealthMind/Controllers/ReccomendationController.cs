using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.RecommendationV;

namespace WealthMind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Recommendation")]
    public class ReccomendationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRecommendationService _recommendationService;

        public ReccomendationController(IMapper mapper, IRecommendationService recomendationService)
        {
            _mapper = mapper;
            _recommendationService = recomendationService;
        }

        [HttpPost("add")]
        [SwaggerOperation(
            Summary = "Registers a recommendation.",
            Description = "Recieves the necessary parameters for registering a recommendation."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SaveRecommendationViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(SaveRecommendationViewModel dto)
        {

            var results = await _recommendationService.Add(dto);
            if (results.HasError)
            {
                return BadRequest(results);
            }
            return Ok(results);

        }

        [HttpGet("get")]
        [SwaggerOperation(
            Summary = "Gets a recommendation.",
            Description = "Recieves the necessary parameters for getting a recommendation."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveRecommendationViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string id)
        {
            var results = await _recommendationService.GetByIdSaveViewModel(id);
            if (results.HasError)
            {
                return BadRequest(results);
            }
            return Ok(results);
        }

        [HttpPut("update")]
        [SwaggerOperation(
            Summary = "Updates a recommendation.",
            Description = "Recieves the necessary parameters for updating a recommendation."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveRecommendationViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(RecommendationViewModel dto, string id)
        {
            var request = _mapper.Map<SaveRecommendationViewModel>(dto);

            try
            {
                await _recommendationService.Update(request, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("delete")]
        [SwaggerOperation(
            Summary = "Deletes a recommendation.",
            Description = "Recieves the necessary parameters for deleting a recommendation."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _recommendationService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
