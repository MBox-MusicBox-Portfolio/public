using System.IdentityModel.Tokens.Jwt;

namespace MBox.Services
{
    public class AuthorisationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthorisationMiddleware> _logger;
        public AuthorisationMiddleware(RequestDelegate next, ILogger<AuthorisationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenRead = tokenHandler.ReadJwtToken(token);
                    var userRoleClaim = tokenRead.Claims.FirstOrDefault(claim => claim.Type == "Role");
                    if (userRoleClaim != null && (userRoleClaim.Value == "user"))
                    {
                        context.Items["IsUser"] = (userRoleClaim.Value == "user");
                        await _next(context);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while Authorisation");
                }
            }

            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
    }
}
