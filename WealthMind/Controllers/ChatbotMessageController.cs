using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services;
using WealthMind.Core.Application.ViewModels.ChatbotMessage;

namespace WealthMind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Developer,Admin,User")]
    [SwaggerTag("Chatbot Messages")]
    public class ChatbotMessageController : ControllerBase
    {
        private readonly IChatbotMessageService _chatbotMessageService;

        public ChatbotMessageController(IChatbotMessageService chatbotMessageService)
        {
            _chatbotMessageService = chatbotMessageService;
        }

        [HttpGet("session/{sessionId}")]
        [SwaggerOperation(Summary = "Get all messages for a specific chat session")]
        [ProducesResponseType(typeof(List<ChatbotMessageViewModel>), 200)]
        public async Task<IActionResult> GetBySessionId(string sessionId)
        {
            try
            {
                var messages = await _chatbotMessageService.GetBySessionIdAsync(sessionId);
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add a new message to a chat session")]
        [ProducesResponseType(typeof(ChatbotMessageViewModel), 201)]
        public async Task<IActionResult> Create([FromBody] SaveChatbotMessageViewModel viewModel)
        {
            try
            {
                var result = await _chatbotMessageService.AddAsync(viewModel);
                return CreatedAtAction(nameof(GetBySessionId), new { sessionId = result.SessionId }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a specific message by ID")]
        [ProducesResponseType(typeof(ChatbotMessageViewModel), 200)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var message = await _chatbotMessageService.GetByIdAsync(id);
                if (message == null)
                    return NotFound();

                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a message")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _chatbotMessageService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ----------------------------------------------------------------------------------------------------------------------

        [HttpGet("getall")]
        [SwaggerOperation(
           Summary = "Gets all chatbot messages",
           Description = "Recieves the necessary parameters for getting all chatbot messages."
       )]
        [Consumes(MediaTypeNames.Application.Json)]
        // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SaveCashViewModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, Developer")]
        public async Task<IActionResult> GetAll()
        {
            var results = await _chatbotMessageService.GetAllViewModel();
            if (results.Any())
            {
                return Ok(results);
            }

            return NotFound();
        }


        [HttpGet("getallbyuser")]
        [SwaggerOperation(
           Summary = "Gets all categories by user ID",
           Description = "Recieves the necessary parameters for getting all categories by user."
       )]
        [Consumes(MediaTypeNames.Application.Json)]
        // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SaveCashViewModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            var results = await _chatbotMessageService.GetAllByUserIdAsync(userId);
            if (results.Any())
            {
                return Ok(results);
            }

            return NotFound();
        }
    }
}
