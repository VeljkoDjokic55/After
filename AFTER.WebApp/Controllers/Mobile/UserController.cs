using AFTER.BLL.Services.Interfaces;
using AFTER.Shared.Common;
using AFTER.Shared.Constants;
using AFTER.Shared.DTOs.User.DataIn;
using AFTER.WebApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AFTER.WebApp.Controllers.Mobile
{
   
    public class UserController : BaseControllerMobile
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public UserController(IAuditLogService auditLogService, IUserService userService, IEmailService emailService)
        {
            _auditLogService = auditLogService;
            _userService = userService;
            _emailService = emailService;

        }

        [HttpPost("forgotPasswordRequest")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            await _auditLogService.Save("/", "mobile/ForgotPassword", email);
            return Ok(await _userService.ForgotPassword(email));
        }

        [HttpPost("resetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDataIn dataIn)
        {
            await _auditLogService.Save("/", "mobile/ResetPassword", dataIn);
            return Ok(await _userService.ResetPasswordByRole(dataIn.Email, dataIn.Password, dataIn.Code));
        }
    }
}
