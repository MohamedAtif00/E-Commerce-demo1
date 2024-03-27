using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using E_Commerce.Application;
using E_Commerce.Application.Common;
using AutoMapper;
using E_Commerce.Application.Authentication;
using E_Commerce.Contract.Authentication.Register.Request;
using E_Commerce.Contract.Authentication.CheckUsername.Request;
using MediatR;
using E_Commerce.Application.User.AddNewUser;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public readonly IDataProtectionProvider _idp;
        public readonly IHttpContextAccessor _contextAccessor;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AuthenticationController(IDataProtectionProvider idp, IHttpContextAccessor contextAccessor, IAuthenticationService authenticationService, IMapper mapper, IMediator mediator)
        {
            _idp = idp;
            _contextAccessor = contextAccessor;
            _authenticationService = authenticationService;
            _mapper = mapper;
            _mediator = mediator;
        }


        // POST api/<AuthenticationController>
        [HttpPost("Register")]
        
        public async Task<IActionResult> Register([FromBody] RegisterRequest register)
        {
            var result = await _mediator.Send(new AddNewUserCommand(register.FirstName,register.LastName,register.Username,register.Email,register.Password,register.PhoneNumber,register.Role));

            return Ok(result);
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequest register)
        {
            var jwtTokens = await _authenticationService.Login(_mapper.Map<LoginDto>(register));

            return Ok(jwtTokens);
        }

        [HttpGet("{id}/{Token}")]
        public async Task<IActionResult> ConformEmail(string id,string Token)
        {
            var result = await _authenticationService.ConfirmEmail(id,Token);

            return Ok(result);
        }

        [HttpPost("checkusername")]
        public async Task<IActionResult> CheckUsername( CheckUsernameRequest username)
        {
            var result = await _authenticationService.CheckUsername(username.username);

            return Ok(result);
        }

        // PUT api/<AuthenticationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthenticationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
