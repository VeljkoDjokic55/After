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
        private readonly IPdfService _pdfService;


        public TicketService(IUnitOfWork unitOfWork, IMapper mapper, IPdfService pdfService)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _pdfService = pdfService;
        }

        public async Task<ResponsePackage<string>> Generate(int count, DateTime? validFrom, DateTime? validTo)
        {

            List<Ticket> tickets = new List<Ticket>();
            for (int i = 0; i < count; i++) 
            {
                tickets.Add(new Ticket()
                {
                    LastUpdateTime = DateTime.Now,
                    Name = string.Empty,
                    ValidFrom = validFrom,
                    ValidTo = validTo,
                    Guid = Guid.NewGuid(),
                    ScannedCount = 0,
                    ScannedCountMax = 1
                });
            }

            await _uow.GetTicketRepository().AddRangeAsync(tickets);
            await _uow.CompleteAsync();

            foreach (var t in tickets) 
            {
                t.Name = $"After_{t.Id.ToString("D10")}";
                await _pdfService.GeneratePdf($"https://after.azurewebsites.net/validate-ticket/{t.Guid}", t.Name);

            }
            await _uow.CompleteAsync();


            return new ResponsePackage<string>(ResponseStatus.OK, "Ticket saved Successfully.");
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
