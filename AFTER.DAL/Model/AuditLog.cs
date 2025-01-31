using System;
using System.ComponentModel.DataAnnotations;

namespace AFTER.DAL.Model
{
    public class AuditLog : Entity
    {
        public DateTime Time { get; set; }
        public User User { get; set; }

        public int? UserId { get; set; }

        [StringLength(50)]
        public string Page { get; set; }

        [StringLength(100)]
        public string Action { get; set; }

        public string Request { get; set; }

    }
}
