using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.SavingV;

namespace WealthMind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Saving")]
    public class SavingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISavingService _savingService;

        public SavingController(IMapper mapper, ISavingService savingService)
        {
            _mapper = mapper;
            _savingService = savingService;
        }

        [HttpPost("add")]
        [SwaggerOperation(
            Summary = "Registers a cash transaction.",
            Description = "Recieves the necessary parameters for registering a cash transaction."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SaveSavingViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(SaveSavingViewModel dto)
        {

            var results = await _savingService.Add(dto);
            if (results.HasError)
            {
                return BadRequest(results);
            }
            return Ok(results);

        }

        [HttpGet("get")]
        [SwaggerOperation(
            Summary = "Gets a cash transaction.",
            Description = "Recieves the necessary parameters for getting a cash transaction."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveSavingViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string id)
        {
            var results = await _savingService.GetByIdSaveViewModel(id);
            if (results.HasError)
            {
                return BadRequest(results);
            }
            return Ok(results);
        }

        [HttpGet("getall")]
        [SwaggerOperation(
            Summary = "Gets all cash transactions.",
            Description = "Recieves the necessary parameters for getting all cash transactions."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var results = await _savingService.GetAllViewModel();
            if (results.Any())
            {
                return Ok(results);
            }

            return NotFound();
        }

        [HttpPut("update")]
        [SwaggerOperation(
            Summary = "Updates a cash transaction.",
            Description = "Recieves the necessary parameters for updating a cash transaction."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveSavingViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(SavingViewModel dto, string id)
        {
            var request = _mapper.Map<SaveSavingViewModel>(dto);

            try
            {
                await _savingService.Update(request, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("delete")]
        [SwaggerOperation(
            Summary = "Deletes a cash transaction.",
            Description = "Recieves the necessary parameters for deleting a cash transaction."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _savingService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
