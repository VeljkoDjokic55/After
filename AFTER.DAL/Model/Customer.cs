using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.DAL.Model
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string Address { get; set; }
        public Dt Dt { get; set; }
        public int? DtId { get; set; }
        public Building Building { get; set; }
        public int? BuildingId { get; set; }
        public Area Area { get; set; }
        public int? AreaId { get; set; }
        public bool IsNew { get; set; }
        public string AccountName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Tariff { get; set; }
        public Meter Meter { get; set; }
        public int? MeterId { get; set; }


    }
}
