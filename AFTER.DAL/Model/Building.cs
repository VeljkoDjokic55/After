using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.DAL.Model
{
    public class Building : Entity
    {
        public Slrn Slrn { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid Guid { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
