using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.DAL.Model
{
    public class Dt :Entity
    {
        public Slrn Slrn { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public Area Area { get; set; }
        public int? AreaId { get; set; }
        public Guid Guid { get; set; }
        public int? Feeder11Id { get; set; }
        public Feeder11 Feeder11 { get; set; }
        public int? Feeder33Id { get; set; }
        public Feeder33 Feeder33 { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
