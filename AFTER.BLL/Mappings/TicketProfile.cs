using AutoMapper;
using AFTER.DAL.Model;
using AFTER.Shared.DTOs.User.DataIn;
using AFTER.Shared.DTOs.User.DataOut;
using AFTER.Shared.DTOs.Ticket.DataIn;

namespace AFTER.BLL.Mappings
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketDataIn>().ReverseMap() ;

        }
    }
}
