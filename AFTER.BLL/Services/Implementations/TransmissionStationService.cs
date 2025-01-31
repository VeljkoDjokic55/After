using AutoMapper;
using AFTER.BLL.Services.Interfaces;
using AFTER.DAL.Model;
using AFTER.DAL.UOWs.Interfaces;
using AFTER.Shared.Common;
using AFTER.Shared.Constants;
using AFTER.Shared.DTOs.Pagination;
using AFTER.Shared.DTOs.TS;
using AFTER.Shared.DTOs.TS.DataIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.BLL.Services.Implementations
{
    public class TransmissionStationService : ITransmissionStationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TransmissionStationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponsePackage<PaginationDataOut<TransmissionStationDto>>> GetAll(TransmissionStationPageInfo dataIn)
        {
            var data = await _uow.GetTransmissionStationRepository().GetAll(dataIn);
            var dataDto = _mapper.Map<List<TransmissionStationDto>>(data.Data);
            return new ResponsePackage<PaginationDataOut<TransmissionStationDto>>()
            {
                Data = new PaginationDataOut<TransmissionStationDto>()
                {
                    Count = data.Count,
                    Data = dataDto
                }
            };
        }

        public async Task<ResponsePackage<string>> Delete(TransmissionStationDto ts)
        {
            ResponsePackage<string> retval = new ResponsePackage<string>("1", ResponseStatus.OK, "Transmission station deleted successfully.");
            var transmissionStation = await _uow.GetTransmissionStationRepository().GetByIdAsync(ts.Id);

            if (transmissionStation != null)
            {
                transmissionStation.IsDeleted = true;
                transmissionStation.LastUpdateTime = DateTime.Now;
            }
            else
            {
                retval.Status = ResponseStatus.NotFound;
                retval.Message = "Transmission station doesn't exist anymore.";
                retval.Data = "2"; //response code
            }

            await _uow.CompleteAsync();

            return retval;
        }

        public async Task<ResponsePackage<string>> Save(TransmissionStationDto ts)
        {
            ResponsePackage<string> retval = new ResponsePackage<string>("1", ResponseStatus.OK, "Transmission station saved successfully.");
            var transmissionStation = _mapper.Map<TransmissionStation>(ts);

            if(transmissionStation.Id == 0)
            {
                var tsInDbByName = await _uow.GetTransmissionStationRepository().GetByName(ts.Name);
                if(tsInDbByName != null)
                {
                    retval.Status = ResponseStatus.BadRequest;
                    retval.Message = "Transmission station with given name already exists!";
                    retval.Data = "2"; //response code
                }
                else
                {
                    transmissionStation.LastUpdateTime = DateTime.Now;
                    transmissionStation.IsDeleted = false;
                    await _uow.GetTransmissionStationRepository().AddAsync(transmissionStation);
                }
            }
            else
            {
                var tsInDb = await _uow.GetTransmissionStationRepository().GetByIdAsync(transmissionStation.Id);
                if(tsInDb != null)
                {
                    tsInDb.Name = transmissionStation.Name;
                    tsInDb.Code = transmissionStation.Code;
                    tsInDb.LastUpdateTime = DateTime.Now;
                }
                else
                {
                    retval.Status = ResponseStatus.NotFound;
                    retval.Message = "Transmission station doesn't exist anymore.";
                    retval.Data = "3"; //response code
                }
            }

            await _uow.CompleteAsync();

            return retval;
        }
    }
}
