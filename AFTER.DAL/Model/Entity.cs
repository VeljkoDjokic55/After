using System;

namespace AFTER.DAL.Model
{
    public class Entity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastUpdateTime { get; set; }
        public int? UtilityId { get; set; }
    }
}
