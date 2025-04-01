using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using WealthMind.Core.Application.DTOs.Account;
using WealthMind.Core.Application.Enums;
using WealthMind.Core.Application.Interfaces.Services;

namespace RoyalState.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Account management")]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        [SwaggerOperation(
         Summary = "User LogIn",
         Description = "Authenticates an user and returns a JWT token."
     )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateWebApiAsync(request));
        }

        [HttpPost("register")]
        [SwaggerOperation(
             Summary = "Registers an user",
             Description = "Recieves the necessary parameters for creating an user"
         )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO dto)
        {
            var origin = Request.Headers["origin"];
            var request = _mapper.Map<RegisterRequest>(dto);
            request.Role = (int)Roles.User;

            var results = await _accountService.RegisterUserAsync(_mapper.Map<RegisterRequest>(request), origin);
            if (results.HasError)
            {
                return BadRequest(results);
            }
            return Ok(results);

        }

        [HttpGet("getById")]
        [SwaggerOperation(
             Summary = "Gets an user",
             Description = "Recieves the necessary parameters for getting an user"
         )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "Admin, Developer")]
        public async Task<IActionResult> GetUser([FromQuery] string Id)
        {
            var results = await _accountService.FindByIdAsync(Id);
            if (results == null)
            {
                return BadRequest("User not found");
            }

            return Ok(results);
        }

        [HttpGet("get-all")]
        [SwaggerOperation(
             Summary = "Gets all users",
             Description = "Recieves the necessary parameters for getting all users"
         )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "Admin, Developer")]
        public async Task<IActionResult> GetAllUsers()
        {
            var results = await _accountService.GetAllUsersAsync();
            if (results == null)
            {
                return BadRequest("No users found");
            }

            return Ok(results);
        }

        [HttpPut("update")]
        [SwaggerOperation(
             Summary = "Updates an user",
                Description = "Recieves the necessary parameters for updating an user"
            )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "Admin, Developer, User")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest dto)
        {
            var results = await _accountService.UpdateUserAsync(dto);
            if (results.HasError)
            {
                return BadRequest(results);
            }

            return Ok(results);

        }

        [HttpDelete("delete")]
        [SwaggerOperation(
             Summary = "Deletes an user",
             Description = "Recieves the necessary parameters for deleting an user"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "Admin, Developer")]
        public async Task<IActionResult> DeleteUser([FromQuery] string Id)
        {
            var results = await _accountService.DeleteUserAsync(Id);
            if (results.HasError)
            {
                return BadRequest(results);
            }
            return Ok(results);

        }

        [HttpPatch("updateStatus/{username}")]
        [SwaggerOperation(
             Summary = "Updates the status of an user",
             Description = "Recieves the necessary parameters for activate or deactivate an user"
         )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "Admin, Developer, User")]
        public async Task<IActionResult> UpdateStatus([FromBody] string username)
        {
            var results = await _accountService.UpdateUserStatusAsync(username);
            if (results.HasError)
            {
                return BadRequest(results);
            }
            return Ok(results);
        }

        [HttpPost("confirm-email")]
        [SwaggerOperation(
            Summary = "Confirms an email",
            Description = "Confirms an email"
        )]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string token)
        {
            var result = await _accountService.ConfirmAccountAsync(userId, token);
            if (result.Contains("confirmed"))
            {
                return Ok("Confirmed");
            }
            return BadRequest(result);
        }

    }
}
