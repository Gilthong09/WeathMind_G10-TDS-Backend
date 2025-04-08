using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.ChatbotSession;
using WealthMind.Core.Application.Wrappers;

namespace WealthMind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Developer,Admin,User")]
    [SwaggerTag("Chatbot Session")]
    public class ChatbotSessionController : ControllerBase
    {
        private readonly IChatbotSessionService _chatbotSessionService;

        public ChatbotSessionController(IChatbotSessionService chatbotSessionService)
        {
            _chatbotSessionService = chatbotSessionService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all chatbot Session")]
        [ProducesResponseType(typeof(List<ChatbotSessionViewModel>), 200)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var sessions = await _chatbotSessionService.GetAllSessionsWithMessagesAsync();
                return Ok(new Response<List<ChatbotSessionViewModel>> { Data = sessions, Succeeded = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new chatbot session")]
        [ProducesResponseType(typeof(SaveChatbotSessionViewModel), 201)]
        public async Task<IActionResult> Create([FromBody] SaveChatbotSessionViewModel viewModel)
        {
            try
            {
                var result = await _chatbotSessionService.Add(viewModel);
                return Ok(result);



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

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing chatbot session")]
        [ProducesResponseType(typeof(Response<ChatbotSessionViewModel>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(string id, [FromBody] SaveChatbotSessionViewModel viewModel)
        {
            try
            {
                await _chatbotSessionService.Update(viewModel, id);
                return Ok(new Response<ChatbotSessionViewModel> { Data = null, Succeeded = true });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new Response<string> { Message = ex.Message, Succeeded = false });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string> { Message = ex.Message, Succeeded = false });
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a chatbot session")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _chatbotSessionService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        [SwaggerOperation(Summary = "Get all active chatbot sessions for a specific user")]
        [ProducesResponseType(typeof(Response<List<ChatbotSessionViewModel>>), 200)]
        public async Task<IActionResult> GetAllActiveByUserId(string userId)
        {
            try
            {
                var sessions = await _chatbotSessionService.GetAllActiveSessionsByUserIdAsync(userId);
                return Ok(new Response<List<ChatbotSessionViewModel>> { Data = sessions, Succeeded = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
