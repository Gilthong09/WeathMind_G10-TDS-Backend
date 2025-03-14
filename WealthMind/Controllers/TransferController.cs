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

        [HttpPost("register-expense")]
        [SwaggerOperation(
            Summary = "Registers an expense.",
            Description = "Recieves the necessary parameters for registering an expense"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterExpenseDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterExpense([FromBody] RegisterExpenseDto dto)
        {
            try
            {
                await _transferService.RegisterExpenseAsync(dto.FromProductId, dto.Amount, dto.Description);
                return Ok(new { message = "Gasto registrado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpPost("register-income")]
        [SwaggerOperation(
            Summary = "Registers an income.",
            Description = "Recieves the necessary parameters for registering an income"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterIncomeDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterIncome([FromBody] RegisterIncomeDto dto)
        {
            try
            {
                await _transferService.RegisterIncomeAsync(dto.ToProductId, dto.Amount, dto.Description);
                return Ok(new { message = "Ingreso registrado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
