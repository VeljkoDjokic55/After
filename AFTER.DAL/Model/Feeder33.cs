using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.DAL.Model
{
    public class Feeder33 :Entity
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public TransmissionStation TransmissionStation { get; set; }
        public int? TransmissionStationId { get; set; }
    }
}
