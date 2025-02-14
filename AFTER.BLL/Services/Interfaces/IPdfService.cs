﻿using AFTER.Shared.Common;
using AFTER.Shared.Constants;
using AFTER.Shared.DTOs.Pagination;
using AFTER.Shared.DTOs.Ticket.DataIn;
using AFTER.Shared.DTOs.User.DataIn;
using AFTER.Shared.DTOs.User.DataOut;
using System.Threading.Tasks;

namespace AFTER.BLL.Services.Interfaces
{
    public interface IPdfService
    {
        Task<ResponsePackage<string>> GeneratePdf(string qrCode,string fileName);
       
    }
}
