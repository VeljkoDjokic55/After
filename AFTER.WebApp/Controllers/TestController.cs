using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AFTER.WebApp.Controllers
{
    public class TestController : BaseController
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return Ok("Web Controller Ok");
        }
    }
}
