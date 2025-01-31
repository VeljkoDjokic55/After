using AFTER.BLL.Services.Interfaces;
using AFTER.Shared.Common;
using AFTER.Shared.Constants;
using AFTER.Shared.DTOs.Ticket.DataIn;
using AFTER.Shared.DTOs.User.DataIn;
using AFTER.WebApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AFTER.WebApp.Controllers
{
    public class TicketController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly ITicketService _ticketService;

        public TicketController(IAuditLogService auditLogService, ITicketService ticketService)
        {
            _auditLogService = auditLogService;
            _ticketService = ticketService;

        }


        [HttpPost("save")]
        public async Task<IActionResult> Save(TicketDataIn dataIn)
        {
            await _auditLogService.Save("/", "Save", dataIn);
            return Ok(await _ticketService.Save(dataIn));
        }

       
    }
}
