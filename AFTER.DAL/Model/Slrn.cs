using AFTER.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.DAL.Model
{
    public class Slrn : Entity
    {
        public string Value { get; set; }
        public EntityType EntityType { get; set; }
    }
}
