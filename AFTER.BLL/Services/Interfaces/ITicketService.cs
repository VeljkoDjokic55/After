using AFTER.Shared.Common;
using AFTER.Shared.Constants;
using AFTER.Shared.DTOs.Pagination;
using AFTER.Shared.DTOs.Ticket.DataIn;
using AFTER.Shared.DTOs.User.DataIn;
using AFTER.Shared.DTOs.User.DataOut;
using System;
using System.Threading.Tasks;

namespace AFTER.BLL.Services.Interfaces
{
    public interface ITicketService
    {
        Task<ResponsePackage<string>> Save(TicketDataIn dataIn);
        Task<ResponsePackage<string>> Generate(int count, DateTime? validFrom, DateTime? validTo);

    }
}
