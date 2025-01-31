using AFTER.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.DAL.Model
{
    public class Meter : Entity
    {
        public Slrn Slrn { get; set; }
        public MeterType? Type { get; set; }
        public MeterCondition? Condition { get; set; }
        public MeterStatus? Status { get; set; }
        public string Number { get; set; }
    }
}
