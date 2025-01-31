using AFTER.DAL.Model;
using AFTER.Shared.DTOs.Pagination;
using AFTER.Shared.DTOs.TS.DataIn;
using System.Threading.Tasks;

namespace AFTER.DAL.Repositories.Interfaces
{
    public interface ITransmissionStationRepository : IRepository<TransmissionStation>
    {
        Task<PaginationDataOut<TransmissionStation>> GetAll(TransmissionStationPageInfo dataIn);
        Task<TransmissionStation> GetByName(string name);
    }
}
