using System;

namespace AMP.Domain.Entities.Base
{
    public class EntityBase
    {
        // TODO:: Add Redis Caching
        // TODO:: Add docker support
        // TODO:: Change types in migration file (nvarchar(max))
        // TODO:: Integration tests (using a sql server docker instance)

        public int Id { get; set; }
        public DateTime DateCreated { get; private set; } // if id = 0, value is DateTime.UtcNow
        public DateTime DateModified { get; private set; }
        // Entity Status (for soft delete)
    }
}
