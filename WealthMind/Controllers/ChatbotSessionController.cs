using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.ChatbotSession;

namespace WealthMind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                return Ok(await _chatbotSessionService.GetAllViewModel());
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
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(string id, [FromBody] SaveChatbotSessionViewModel viewModel)
        {
            try
            {
                await _chatbotSessionService.Update(viewModel, id);
                return NoContent();
            }
            catch (Exception ex) when (ex.Message.Contains("no encontrado"))
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
    }
}
