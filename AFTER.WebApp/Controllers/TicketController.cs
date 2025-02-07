using AFTER.BLL.Services.Interfaces;
using AFTER.Shared.Common;
using AFTER.Shared.Constants;
using AFTER.Shared.DTOs.Ticket.DataIn;
using AFTER.Shared.DTOs.User.DataIn;
using AFTER.WebApp.Helpers;
using After.BLL.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net.Codecrete.QrCodeGenerator;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.WebApp.Controllers
{
    public class TicketController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly ITicketService _ticketService;
        private readonly IPdfService _pdfService;

        public TicketController(IAuditLogService auditLogService, ITicketService ticketService, IPdfService pdfService)
        {
            _auditLogService = auditLogService;
            _ticketService = ticketService;
            _pdfService = pdfService;
        }


        [HttpPost("save")]
        public async Task<IActionResult> Save(TicketDataIn dataIn)
        {
            await _auditLogService.Save("/", "Save", dataIn);
            return Ok(await _ticketService.Save(dataIn));
        }

        [HttpPost("generateQrCode")]
        [AllowAnonymous]
        public async Task<IActionResult> generateQrCode(string dataIn)
        {
            await _auditLogService.Save("Ticket", "generateQrCode", dataIn);

            QrCode qr = QrCode.EncodeText(string.Empty, QrCode.Ecc.Medium);
            var pathToQrCode = qr.GenerateImage(dataIn,dataIn);

                return Ok();
        }

        [HttpPost("generateTicketPdf")]
        [AllowAnonymous]
        public async Task<IActionResult> generateTicketPdf(string qrCode,string fileName)
        {
            await _auditLogService.Save("Ticket", "generateTicketPdf", qrCode);

            await _pdfService.GeneratePdf(qrCode, fileName);
            return Ok();
        }

        [HttpPost("GenerateTickets")]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateTickets(int count)
        {
            await _auditLogService.Save("Ticket", "GenerateTickets", count);

            var validFrom = new DateTime(2025, 7, 11, 0, 0, 0, DateTimeKind.Utc);
            var validTo = new DateTime(2025, 7, 11, 23, 59, 59, 999, DateTimeKind.Utc);
            await _ticketService.Generate(count, validFrom, validTo);
            return Ok();
        }




    }
}
