using AMP.Domain.Enums;
using System;

namespace AMP.Domain.Entities.Base
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; protected set; } // if id = 0, value is DateTime.UtcNow (Do that in configuration file)
        public DateTime DateModified { get; set; }
        public EntityStatus EntityStatus { get; set; } = EntityStatus.Normal;
        // Entity Status (for soft delete)
    }
}
