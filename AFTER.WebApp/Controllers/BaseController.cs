using AFTER.WebApp.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AFTER.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}
