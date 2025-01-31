using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.DAL.Model
{
    public class Area : Entity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Region Region { get; set; }
        public int? RegionId { get; set; }

    }
}
