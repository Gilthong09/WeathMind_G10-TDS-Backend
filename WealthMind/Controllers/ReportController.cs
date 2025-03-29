using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.ReportV;

namespace WealthMind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Report")]
    public class ReportController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService, IMapper mapper)
        {
            _mapper = mapper;
            _reportService = reportService;
        }

        [HttpPost("add")]
        [SwaggerOperation(
            Summary = "Registers a report.",
            Description = "Recieves the necessary parameters for registering a report."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SaveReportViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(SaveReportViewModel dto)
        {

            var results = await _reportService.Add(dto);
            if (results.HasError)
            {
                return BadRequest(results);
            }
            return Ok(results);

        }

        [HttpGet("get")]
        [SwaggerOperation(
            Summary = "Gets a report.",
            Description = "Recieves the necessary parameters for getting a report."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveReportViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string id)
        {
            var results = await _reportService.GetByIdSaveViewModel(id);
            if (results.HasError)
            {
                return BadRequest(results);
            }
            return Ok(results);
        }

        [HttpPut("update")]
        [SwaggerOperation(
            Summary = "Updates a reports.",
            Description = "Recieves the necessary parameters for updating a reports."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveReportViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(ReportViewModel dto, string id)
        {
            var request = _mapper.Map<SaveReportViewModel>(dto);

            try
            {
                await _reportService.Update(request, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("delete")]
        [SwaggerOperation(
            Summary = "Deletes a report.",
            Description = "Recieves the necessary parameters for deleting a report."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _reportService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
