using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.DAL.Model
{
    public class Feeder11 :Entity
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public Substation Substation { get; set; }
        public int? SubstationId { get; set; }
    }
}
