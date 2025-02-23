using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WealthMind.Core.Application.DTOs.Account;
using WealthMind.Core.Application.Enums;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.User;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RoyalState.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Membership System")]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper, IUserService userService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [SwaggerOperation(
         Summary = "User LogIn",
         Description = "Authenticates an user and returns a JWT token."
     )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateWebApiAsync(request));
        }

        [HttpPost("register")]
        [SwaggerOperation(
             Summary = "Registers an user",
             Description = "Recieves the necessary parameters for creating an user"
         )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterDevAsync(RegisterDTO dto)
        {
            var origin = Request.Headers["origin"];
            var request = _mapper.Map<RegisterRequest>(dto);
            request.Role = (int)Roles.Developer;

            var results = await _userService.RegisterAsync(_mapper.Map<SaveUserViewModel>(request), origin);

            return Ok(results);

        }

        [HttpGet("confirm-email")]
        [SwaggerOperation(
            Summary = "Confirms an email",
            Description = "Confirms an email"
        )]
        public async Task<IActionResult> ConfirmEmailAsync(string userId, string token)
        {
            var result = await _userService.ConfirmEmailAsync(userId, token);
            if (result.Contains("confirmed"))
            {
                return Ok("Confirmed");
            }
            return BadRequest(result); 
        }

    }
}
