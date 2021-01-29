using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using WebRPG.Services;

namespace WebRPG.Filters
{
    public class CheckToken : ActionFilterAttribute
    {
        private ILogger<CheckToken> _logger;
        private ITokenService _tokenService;

        public CheckToken(ILogger<CheckToken> logger, ITokenService tokenService)
        {
            _logger = logger;
            this._tokenService = tokenService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var httpContext = context.HttpContext;
            if (httpContext.Request.Cookies.TryGetValue("token", out var tokenId))
            {
                if (_tokenService.Check(tokenId))
                {
                    await next();
                    return;
                }
            }

            httpContext.Response.Cookies.Append("returnUrl", httpContext.Request.Path);

            context.Result = new RedirectToActionResult("Index", "Login", null);

            // httpContext.Response.Redirect("/login");
        }
    }
}