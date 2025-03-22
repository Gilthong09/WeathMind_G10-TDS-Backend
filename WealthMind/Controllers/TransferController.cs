using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WealthMind.Core.Application.DTOs.Transfer;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services;
using WealthMind.Core.Application.ViewModels.TransactionV;


namespace WealthMind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Transfer")]
    public class TransferController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITransferService _transferService;
        public TransferController(IMapper mapper, ITransferService transferService)
        {
            _mapper = mapper;
            _transferService = transferService;
        }

        [HttpPost("transfer")]
        [SwaggerOperation(
            Summary = "Register a Transaction",
            Description = "Recieves the necessary parameters for registering a transaction.")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTransactionViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Transfer([FromBody] SaveTransactionViewModel trx)
        {
            try
            {
                await _transferService.TransferAsync(trx);
                return Ok(new { message = "Transacción registrada correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
