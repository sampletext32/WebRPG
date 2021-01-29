using Microsoft.AspNetCore.Mvc;
using WebRPG.Filters;

namespace WebRPG.Controllers
{
    [Route("[controller]/{action=Index}")]
    public class TestController : Controller
    {
        [TypeFilter(typeof(CheckToken))]
        public IActionResult Index()
        {
            return Ok("I'm logined");
        }
    }
}
