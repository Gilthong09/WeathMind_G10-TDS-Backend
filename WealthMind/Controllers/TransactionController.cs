using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.TransactionV;

namespace WealthMind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;

        public TransactionController(IMapper mapper, ITransactionService transactionService)
        {
            _mapper = mapper;
            _transactionService = transactionService;
        }

        //[HttpPost("add")]
        //[SwaggerOperation(
        //    Summary = "Registers a transaction.",
        //    Description = "Recieves the necessary parameters for registering a transaction."
        //)]
        //[Consumes(MediaTypeNames.Application.Json)]
        //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SaveTransactionViewModel))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Add(SaveTransactionViewModel dto)
        //{

        //    var results = await _transactionService.Add(dto);
        //    if (results.HasError)
        //    {
        //        return BadRequest(results);
        //    }
        //    return Ok(results);

        //}

        [HttpGet("get")]
        [SwaggerOperation(
            Summary = "Gets a transaction.",
            Description = "Recieves the necessary parameters for getting a transaction."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTransactionViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string id)
        {
            var results = await _transactionService.GetByIdSaveViewModel(id);
            if (results.HasError)
            {
                return BadRequest(results);
            }
            return Ok(results);
        }

        [HttpGet("getall")]
        [SwaggerOperation(
            Summary = "Gets all transactions.",
            Description = "Recieves the necessary parameters for getting all transactions."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var results = await _transactionService.GetAllViewModel();
            if (results.Any())
            {
                return Ok(results);
            }

            return NotFound();
        }

        /*[HttpPut("update")]
        [SwaggerOperation(
            Summary = "Updates a transaction.",
            Description = "Recieves the necessary parameters for updating a transaction."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTransactionViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(TransactionViewModel dto, string id)
        {
            var request = _mapper.Map<SaveTransactionViewModel>(dto);

            try
            {
                await _transactionService.Update(request, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }*/

        [HttpDelete("delete")]
        [SwaggerOperation(
            Summary = "Deletes a transaction transaction.",
            Description = "Recieves the necessary parameters for deleting a transaction."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _transactionService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




        //Gilthong



        [HttpGet("user/{userId}")]
        [SwaggerOperation(
        Summary = "Gets transactions by user ID.",
        Description = "Retrieves all transactions for a specific user."
    )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTransactionsByUserId(string userId)
        {
            var transactions = await _transactionService.GetTransactionsByUserIdAsync(userId);
            return Ok(transactions);
        }

        [HttpGet("category/{categoryId}/user/{userId}")]
        [SwaggerOperation(
            Summary = "Gets transactions by category and user ID.",
            Description = "Retrieves all transactions for a specific category and user."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTransactionsByCategory(string categoryId, string userId)
        {
            var transactions = await _transactionService.GetTransactionsByCategoryAsync(categoryId, userId);
            return Ok(transactions);
        }

        [HttpGet("daterange")]
        [SwaggerOperation(
            Summary = "Gets transactions within a date range.",
            Description = "Retrieves all transactions within a specified date range for a user."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTransactionsByDateRange(string userId, DateTime startDate, DateTime endDate)
        {
            var transactions = await _transactionService.GetTransactionsByDateRangeAsync(userId, startDate, endDate);
            return Ok(transactions);
        }

        [HttpGet("income")]
        [SwaggerOperation(
            Summary = "Gets total income for a user in a month.",
            Description = "Calculates the total income of a user for a specific month and year."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTotalIncome(string userId, int year, int month)
        {
            var totalIncome = await _transactionService.GetTotalIncomeAsync(userId, year, month);
            return Ok(totalIncome);
        }

        [HttpGet("expenses")]
        [SwaggerOperation(
            Summary = "Gets total expenses for a user in a month.",
            Description = "Calculates the total expenses of a user for a specific month and year."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTotalExpenses(string userId, int year, int month)
        {
            var totalExpenses = await _transactionService.GetTotalExpensesAsync(userId, year, month);
            return Ok(totalExpenses);
        }

        [HttpGet("top-expenses")]
        [SwaggerOperation(
            Summary = "Gets top expenses for a user in a month.",
            Description = "Retrieves the top N highest expenses for a user in a specific month and year."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTopExpensesByCategory(string userId, int year, int month, int topN)
        {
            var transactions = await _transactionService.GetTopExpensesByCategoryAsync(userId, year, month, topN);
            return Ok(transactions);
        }
    }
}
