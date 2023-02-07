using AMP.Domain.Enums;
using System;

namespace AMP.Domain.Entities.Base
{
    public class Entity
    {
        public string Id { get; protected set; } = Guid.NewGuid().ToString();
        public int RowId { get; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IgnoreDateModified { get; set; }
        public EntityStatus EntityStatus { get; set; }
    }
}
