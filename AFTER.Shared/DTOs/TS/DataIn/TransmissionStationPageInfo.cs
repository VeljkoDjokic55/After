using AFTER.Shared.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.Shared.DTOs.TS.DataIn
{
    public class TransmissionStationPageInfo
    {
        public PageInfo PageInfo { get; set; }
        public TransmissionStationDataIn FilterParams { get; set; }
    }
}
