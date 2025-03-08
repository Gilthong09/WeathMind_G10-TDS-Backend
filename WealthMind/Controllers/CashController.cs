using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using WealthMind.Core.Application.DTOs.Account;
using WealthMind.Core.Application.Enums;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.CashV;
using WealthMind.Infrastructure.Identity.Services;

namespace WealthMind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Cash")]
    public class CashController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICashService _cashService;

        public CashController(IMapper mapper, ICashService cashService)
        {
            _mapper = mapper;
            _cashService = cashService;
        }

        [HttpPost("add")]
        [SwaggerOperation(
            Summary = "Registers a cash transaction.",
            Description = "Recieves the necessary parameters for registering a cash transaction."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SaveCashViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(SaveCashViewModel dto)
        {
            // var request = _mapper.Map<SaveCashViewModel>(dto);

            var results = await _cashService.Add(dto);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveCashViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string id)
        {
            var results = await _cashService.GetByIdSaveViewModel(id);
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
        // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SaveCashViewModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var results = await _cashService.GetAllViewModel();
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveCashViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(CashViewModel dto, string id)
        {
            var request = _mapper.Map<SaveCashViewModel>(dto);

            try
            {
                await _cashService.Update(request, id);
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
                await _cashService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
