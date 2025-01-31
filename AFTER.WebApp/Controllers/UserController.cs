using AFTER.BLL.Services.Interfaces;
using AFTER.Shared.Common;
using AFTER.Shared.Constants;
using AFTER.Shared.DTOs.User.DataIn;
using AFTER.WebApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AFTER.WebApp.Controllers
{
    public class UserController : BaseController
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
           return Ok(await _userService.ForgotPassword(email));
        }

        [HttpPost("resetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDataIn dataIn)
        {
            await _auditLogService.Save("/", "ResetPassword", dataIn);

            return Ok(await _userService.ResetPasswordByRole(dataIn.Email, dataIn.Password, dataIn.Code));
        }


        [HttpPost("save")]
        [AllowAnonymous]
        public async Task<IActionResult> Save(UserDataIn dataIn)
        {
            await _auditLogService.Save("/", "Save", dataIn);
            return Ok(await _userService.Save(dataIn));
        }

        [HttpPost("getAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(UserPageInfo dataIn)
        {
            await _auditLogService.Save("/", "GetAll", dataIn);
            return Ok(await _userService.GetAll(dataIn));
        }

        [HttpPost("setStatus")]
        [AllowAnonymous]
        public async Task<IActionResult> SetStatus(SetStatusDataIn dataIn)
        {
            await _auditLogService.Save("/", "SetStatus", dataIn);
            return Ok(await _userService.SetStatus(dataIn));
        }
    }
}
