using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AFTER.WebApp.Controllers.Mobile
{
    public class TestController : BaseControllerMobile
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return Ok("Mobile Controller Ok");
        }
    }
}
