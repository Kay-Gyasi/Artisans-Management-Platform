using AMP.Domain.Enums;
using System;

namespace AMP.Domain.Entities.Base
{
    public class EntityBase
    {
        public string Id { get; protected set; } = Guid.NewGuid().ToString();
        public DateTime DateCreated { get; protected set; }
        public DateTime DateModified { get; set; } = DateTime.UtcNow;
        public EntityStatus EntityStatus { get; set; }
    }
}
