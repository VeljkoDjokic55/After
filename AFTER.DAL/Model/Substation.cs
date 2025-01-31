using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.DAL.Model
{
    public class Substation : Entity
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public Feeder33 Feeder { get; set; }
        public int? FeederId { get; set; }
        public virtual ICollection<Feeder33Ss> Feeders { get; set; }

    }
}
