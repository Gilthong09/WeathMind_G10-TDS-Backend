using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.CategoryV;
using WealthMind.Core.Application.ViewModels.FinancialGoal;

namespace WealthMind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("FinancialGoal")]
    public class FinancialGoalController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFinancialGoalService _financialGoalService;

        public FinancialGoalController(IMapper mapper, IFinancialGoalService financialGoalService)
        {
            _mapper = mapper;
            _financialGoalService = financialGoalService;
        }


        [HttpPost("add")]
        [SwaggerOperation(
            Summary = "Registers a financial goal.",
            Description = "Recieves the necessary parameters for registering a financial goal."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SaveFinancialGoalViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(SaveFinancialGoalViewModel dto)
        {

            var results = await _financialGoalService.Add(dto);
            if (results.HasError)
            {
                return BadRequest(results);
            }
            return Ok(results);

        }

        [HttpGet("get")]
        [SwaggerOperation(
            Summary = "Gets a financial goal.",
            Description = "Recieves the necessary parameters for getting a financial goal."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveFinancialGoalViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string id)
        {
            var results = await _financialGoalService.GetByIdSaveViewModel(id);
            if (results.HasError)
            {
                return BadRequest(results);
            }
            return Ok(results);
        }


        [HttpGet("getall")]
        [SwaggerOperation(
           Summary = "Gets all financials goals",
           Description = "Recieves the necessary parameters for getting all financials goals."
       )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FinancialGoalViewModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var results = await _financialGoalService.GetAllViewModel();
            if (results.Any())
            {
                return Ok(results);
            }

            return NotFound();
        }

        [HttpPut("update")]
        [SwaggerOperation(
            Summary = "Updates a financial goal.",
            Description = "Recieves the necessary parameters for updating a financial goal."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveFinancialGoalViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(SaveFinancialGoalViewModel dto, string id)
        {
            var request = _mapper.Map<SaveFinancialGoalViewModel>(dto);

            try
            {
                await _financialGoalService.Update(request, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("delete")]
        [SwaggerOperation(
            Summary = "Deletes a financial goal.",
            Description = "Recieves the necessary parameters for deleting a financial goal."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _financialGoalService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
