using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.DAL.Model
{
    public class Feeder33Ss : Entity
    {
        public Feeder33 Feeder { get; set; }
        public Substation Substation { get; set; }
    }
}
