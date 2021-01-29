#nullable enable
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRPG.ActionResults;
using WebRPG.Services;

namespace WebRPG.Controllers
{
    [Route("[controller]/{action=Index}")]
    public class LoginController : Controller
    {
        private ITokenService _tokenService;

        public LoginController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult Index(string? returnUrl)
        {
            return new HtmlResult(
                $@"<form action=""./login/auth"" method=""get"">
                    <input type=""submit"" value=""Auth""/>
                </form>"
            );
        }

        [HttpGet]
        public IActionResult Auth()
        {
            if (HttpContext.Request.Cookies.TryGetValue("returnUrl", out var returnUrl))
            {
                HttpContext.Response.Cookies.Delete("returnUrl");
            }

            if (HttpContext.Request.Cookies.TryGetValue("token", out var tokenId))
            {
                if (_tokenService.Check(tokenId))
                {
                    return Redirect(!string.IsNullOrEmpty(returnUrl) ? returnUrl : "/test");
                }

                HttpContext.Response.Cookies.Delete("token");
            }

            var token = _tokenService.Create();
            HttpContext.Response.Cookies.Append("token", token);
            return Redirect(!string.IsNullOrEmpty(returnUrl) ? returnUrl : "/test");
        }
    }
}