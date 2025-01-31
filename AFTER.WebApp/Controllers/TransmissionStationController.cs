using AFTER.BLL.Services.Interfaces;
using AFTER.Shared.DTOs.TS;
using AFTER.Shared.DTOs.TS.DataIn;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AFTER.WebApp.Controllers
{
    public class TransmissionStationController : BaseController
    {
        private readonly IAuditLogService _auditLogService;
        private readonly ITransmissionStationService _transmissionStationService;

        public TransmissionStationController(IAuditLogService auditLogService, ITransmissionStationService transmissionStationService)
        {
            _auditLogService = auditLogService;
            _transmissionStationService = transmissionStationService;
        }

        [HttpPost("getAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(TransmissionStationPageInfo dataIn)
        {
            await _auditLogService.Save("TransmissionStation", "GetAll", dataIn);
            return Ok(await _transmissionStationService.GetAll(dataIn));
        }

        [HttpPost("save")]
        [AllowAnonymous]
        public async Task<IActionResult> Save(TransmissionStationDto ts)
        {
            await _auditLogService.Save("TransmissionStation", "Save", ts);
            return Ok(await _transmissionStationService.Save(ts));
        }

        [HttpPost("delete")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(TransmissionStationDto ts)
        {
            await _auditLogService.Save("TransmissionStation", "Delete", ts);
            return Ok(await _transmissionStationService.Delete(ts));
        }
    }
}
