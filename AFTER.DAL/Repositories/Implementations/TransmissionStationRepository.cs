using AFTER.DAL.Context;
using AFTER.DAL.Model;
using AFTER.DAL.Repositories.Interfaces;
using AFTER.Shared.DTOs.Pagination;
using AFTER.Shared.DTOs.TS.DataIn;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AFTER.DAL.Repositories.Implementations
{
    public class TransmissionStationRepository : Repository<TransmissionStation>, ITransmissionStationRepository
    {
        public AFTERContext AFTERContext
        {
            get { return _dbContext as AFTERContext; }
        }

        public TransmissionStationRepository(AFTERContext context) : base(context)
        {
        }

        public async Task<PaginationDataOut<TransmissionStation>> GetAll(TransmissionStationPageInfo dataIn)
        {
            var query = GetPagingQuery(dataIn);
            return new PaginationDataOut<TransmissionStation>()
            {
                Count = query.Count(),
                Data = await query
                    .OrderByDescending(x => x.Id)
                    .Skip((dataIn.PageInfo.Page - 1) * dataIn.PageInfo.PageSize)
                    .Take(dataIn.PageInfo.PageSize)
                    .AsNoTracking()
                    .ToListAsync()
            };
        }

        private IQueryable<TransmissionStation> GetPagingQuery(TransmissionStationPageInfo dataIn)
        {
            var query = _dbContext.Set<TransmissionStation>()
                .Where(x => x.IsDeleted == false);

            if (!string.IsNullOrWhiteSpace(dataIn.FilterParams.SearchValue))
                query = query.Where(x => x.Name.ToUpper().Contains(dataIn.FilterParams.SearchValue.ToUpper())
                        || x.Code.ToUpper().Contains(dataIn.FilterParams.SearchValue.ToUpper()));

            return query;
        }

        public async Task<TransmissionStation> GetByName(string name)
        {
            return await _dbContext.Set<TransmissionStation>()
                .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Name == name);
        }

    }
}
