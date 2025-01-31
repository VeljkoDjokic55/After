using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.DAL.Model
{
    public class Ticket : Entity
    {
        public Guid? Guid { get; set; }
        public string Name { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get;set; }

    }
}
