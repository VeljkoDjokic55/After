using AFTER.Shared.Common;
using AFTER.Shared.DTOs.Pagination;
using AFTER.Shared.DTOs.TS;
using AFTER.Shared.DTOs.TS.DataIn;
using System.Threading.Tasks;

namespace AFTER.BLL.Services.Interfaces
{
    public interface ITransmissionStationService
    {
        Task<ResponsePackage<PaginationDataOut<TransmissionStationDto>>> GetAll(TransmissionStationPageInfo dataIn);
        Task<ResponsePackage<string>> Save(TransmissionStationDto ts);
        Task<ResponsePackage<string>> Delete(TransmissionStationDto ts);
    }
}
