using AMP.Domain.Enums;
using System;

namespace AMP.Domain.Entities.Base
{
    public class EntityBase
    {
        public string Id { get; protected set; } = Guid.NewGuid().ToString();
        public int RowId { get; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public EntityStatus EntityStatus { get; set; }
    }
}
