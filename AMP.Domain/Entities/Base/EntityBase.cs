using AMP.Domain.Enums;
using System;

namespace AMP.Domain.Entities.Base
{
    public class EntityBase
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime DateCreated { get; protected set; } // if id = 0, value is DateTime.UtcNow (Do that in configuration file)
        public DateTime DateModified { get; set; } = DateTime.UtcNow;
        public EntityStatus EntityStatus { get; set; }
        // Entity Status (for soft delete)
    }
}
