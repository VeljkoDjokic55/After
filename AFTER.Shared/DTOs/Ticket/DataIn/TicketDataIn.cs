using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.Shared.DTOs.Ticket.DataIn
{
    public class TicketDataIn
    {
        public int Id { get; set; }
        public Guid? Guid { get; set; }
        public string Name { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
