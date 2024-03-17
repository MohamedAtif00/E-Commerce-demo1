using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using E_Commerce.Application;
using E_Commerce.Application.Common;
using E_Commerce.Contract.Register.Request;
using AutoMapper;
using E_Commerce.Application.Authentication;

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

        public AuthenticationController(IDataProtectionProvider idp, IHttpContextAccessor contextAccessor, IAuthenticationService authenticationService, IMapper mapper)
        {
            _idp = idp;
            _contextAccessor = contextAccessor;
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<IActionResult> Register()
        //{

        //}

        [HttpGet("login")]
        public async Task<string> Login()
        {
            //var claims = new List<Claim>();
            //claims.Add(new Claim("user", "Mohamed"));
            //var identity = new ClaimsIdentity(claims, "cookie");
            //var principle = new ClaimsPrincipal(identity);
            //await _contextAccessor.HttpContext.SignInAsync("cookie", principle);
            ////var protector = _idp.CreateProtector("auth-cookie");
            ////var ctx = HttpContext.Response.Headers["set-cookie"] = $"auth={protector.Protect("username:Ahmed")}";
            ////Response.Cookies.Append("set-cookie", $"auth={protector.Protect("username:Ahmed")}", new CookieOptions { Expires = DateTime.Now.AddDays(1)});
            //return "value";
            return string.Empty;
        }

        // POST api/<AuthenticationController>
        [HttpPost("Register")]
        
        public async Task<IActionResult> Register([FromBody] RegisterRequest register)
        {
            var jwtTokens = await _authenticationService.Register(_mapper.Map<RegisterDto>(register));

            return Ok(jwtTokens);
        }

        [HttpGet("{id}/{Token}")]
        public async Task<IActionResult> ConformEmail(string id,string Token)
        {
            var result = await _authenticationService.ConfirmEmail(id,Token);

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
