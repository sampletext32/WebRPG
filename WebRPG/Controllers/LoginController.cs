using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult Index()
        {
            return Ok("Login page");
        }

        [HttpGet]
        public IActionResult Auth()
        {
            if (HttpContext.Request.Cookies.TryGetValue("token", out var tokenId))
            {
                if (_tokenService.Check(tokenId))
                {
                    return Ok("Already have a token: " + tokenId);
                }

                HttpContext.Response.Cookies.Delete("token");
            }

            var token = _tokenService.Create();
            HttpContext.Response.Cookies.Append("token", token);
            return Ok(token);
        }
    }
}