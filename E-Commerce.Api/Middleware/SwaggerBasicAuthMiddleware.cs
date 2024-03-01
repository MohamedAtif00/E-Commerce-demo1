using System.Net;
using System.Text;

namespace E_Commerce.Api.Middleware
{
    public class SwaggerBasicAuthMiddleware
    {
        public readonly RequestDelegate next;
        public readonly IConfiguration Config;

        public SwaggerBasicAuthMiddleware(RequestDelegate next, IConfiguration config)
        {
            this.next = next;
            Config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/Swagger"))
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic"))
                {
                    var EncodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(EncodedUsernamePassword));

                    var usernaem = decodedUsernamePassword.Split(':', 2)[0];
                    var password = decodedUsernamePassword.Split(":", 2)[1];

                    if (IsAuthorized(usernaem, password))
                    {
                        next.Invoke(context);
                    }
                }

                context.Response.Headers["WWW-Authentication"] = "Basic";

                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            }
            else
            {
                next.Invoke(context);
            }
        }

        public bool IsAuthorized(string username,string password)
        {
            var _username = Config["SwaggerSetting:SwaggerSetting_UserName"];
            var _password = Config["SwaggerSetting:SwaggerSetting_Password"];

            return username.Equals(_username,StringComparison.InvariantCultureIgnoreCase) && password.Equals(_password);
        }

    }

}
