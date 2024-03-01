using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public readonly IDataProtectionProvider _idp;
        public readonly IHttpContextAccessor _contextAccessor;

        public AuthenticationController(IDataProtectionProvider idp, IHttpContextAccessor contextAccessor)
        {
            _idp = idp;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public async Task<string> UserName()
        {
            var protector = _idp.CreateProtector("auth-cookie");

            var cookie = HttpContext.Request.Headers.Cookie.FirstOrDefault(x => x.StartsWith("auth="));
            var payload = cookie.Split("=").Last();
            var afterProtection = protector.Unprotect(payload);
            var parts = afterProtection.Split(':');
            var key = parts[0];
            var value = parts[1];
            return value;
        }

        [HttpGet("login")]
        public async Task<string> Login()
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("user", "Mohamed"));
            var identity = new ClaimsIdentity(claims, "cookie");
            var principle = new ClaimsPrincipal(identity);
            await _contextAccessor.HttpContext.SignInAsync("cookie", principle);
            //var protector = _idp.CreateProtector("auth-cookie");
            //var ctx = HttpContext.Response.Headers["set-cookie"] = $"auth={protector.Protect("username:Ahmed")}";
            //Response.Cookies.Append("set-cookie", $"auth={protector.Protect("username:Ahmed")}", new CookieOptions { Expires = DateTime.Now.AddDays(1)});
            return "value";
        }

        // POST api/<AuthenticationController>
        [HttpPost]
        
        public void Post([FromBody] string value)
        {
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
