using AutoMapper;
using AFTER.BLL.Services.Interfaces;
using AFTER.DAL.Model;
using AFTER.DAL.UOWs.Interfaces;
using AFTER.Shared.Common;
using AFTER.Shared.Constants;
using AFTER.Shared.DTOs.Pagination;
using AFTER.Shared.DTOs.User.DataIn;
using AFTER.Shared.DTOs.User.DataOut;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AFTER.Shared.DTOs.Ticket.DataIn;

namespace AFTER.BLL.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;


        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

       

        public async Task<ResponsePackage<string>> Save(TicketDataIn dataIn)
        {
            ResponsePackage<string> retval = new ResponsePackage<string>(ResponseStatus.OK, "Ticket saved Successfully.");
            var ticket = _mapper.Map<Ticket>(dataIn);

            if (ticket.Id == 0)
            {
                ticket.Guid = Guid.NewGuid();
                await _uow.GetTicketRepository().AddAsync(ticket);
            }
            else
            {
                var ticketInDb = await _uow.GetTicketRepository().GetByIdAsync(ticket.Id);
                if (ticketInDb != null)
                {
                    ticketInDb.LastUpdateTime = DateTime.Now;
                    ticketInDb.Name = ticket.Name;
                    ticketInDb.ValidFrom = ticket.ValidFrom;
                    ticketInDb.ValidTo = ticket.ValidTo;
                    ticketInDb.Guid = ticket.Guid;

                }
                else
                {
                    retval.Status = ResponseStatus.NotFound;
                    retval.Message = "Ticket doesn`t exist anymore.";
                }

            }

            await _uow.CompleteAsync();

            return retval;
        }
    }
}
