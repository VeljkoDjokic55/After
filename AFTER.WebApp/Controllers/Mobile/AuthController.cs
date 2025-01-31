using AFTER.BLL.Services.Interfaces;
using AFTER.DAL.Model;
using AFTER.Shared.Common;
using AFTER.Shared.Constants;
using AFTER.Shared.DTOs.User.DataIn;
using AFTER.WebApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AFTER.WebApp.Controllers.Mobile
{
    public class AuthController : BaseControllerMobile
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IUserService _userService;

        public AuthController(IAuditLogService auditLogService, IUserService userService)
        {
            _auditLogService = auditLogService;
            _userService = userService;
        }
       
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginData loginData)
        {
            ResponsePackage<string> retval = new ResponsePackage<string>();

            var userServiceResponse = await _userService.Get(loginData.Email, loginData.Password);

            if (userServiceResponse.Status != ResponseStatus.OK)
            {
                retval.Status = userServiceResponse.Status;
                retval.Message = userServiceResponse.Message;
            }
            else
            {
                var user = userServiceResponse.Data;
                if (user == null)
                    retval = new ResponsePackage<string>(ResponseStatus.BadRequest, "Wrong email or password.");
                else
                {
                    string token = JwtManager.GetToken(user, 60);
                    retval = new ResponsePackage<string>(token);
                }
            }

            await _auditLogService.Save("/", "mobile/Login", loginData);
            return Ok(retval);
        }
    }
}
