using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.ChatbotMessage;

namespace WealthMind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Chatbot Message")]
    public class ChatbotMessageController : ControllerBase
    {
        private readonly IChatbotMessageService _chatbotMessageService;

        public ChatbotMessageController(IChatbotMessageService ChatbotMessageService)
        {
            _chatbotMessageService = ChatbotMessageService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all chatbot messages")]
        [ProducesResponseType(typeof(List<ChatbotMessageViewModel>), 200)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _chatbotMessageService.GetAllViewModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get chatbot message by ID")]
        [ProducesResponseType(typeof(ChatbotMessageViewModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var saveproduct = await _chatbotMessageService.GetByIdSaveViewModel(id);

                var chatbotMessage = new ChatbotMessageViewModel
                {
                    SessionId = saveproduct.SessionId,
                    UserMessage = saveproduct.UserMessage,
                    BotResponse = saveproduct.BotResponse,
                    Timestamp = saveproduct.Timestamp,
                    Session = saveproduct.Session
                };


                if (chatbotMessage == null)
                    return NotFound();
                return Ok(chatbotMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new chatbot message")]
        [ProducesResponseType(typeof(SaveChatbotMessageViewModel), 201)]
        public async Task<IActionResult> Create([FromBody] SaveChatbotMessageViewModel viewModel)
        {
            try
            {
                var result = await _chatbotMessageService.Add(viewModel);
                return CreatedAtAction(nameof(GetById), new { id = result.SessionId }, result);
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
        [SwaggerOperation(Summary = "Update an existing chatbot message")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(string id, [FromBody] SaveChatbotMessageViewModel viewModel)
        {
            try
            {
                await _chatbotMessageService.Update(viewModel, id);
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
        [SwaggerOperation(Summary = "Delete a chatbot message")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _chatbotMessageService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
