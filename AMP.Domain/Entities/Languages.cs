using System.Collections.Generic;
using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities
{
    public sealed class Languages : EntityBase
    {
        private Languages(string name)
        {
            Name = name;
        }

        public static Languages Create(string name) 
            => new Languages(name);

        public Languages WithName(string name)
        {
            Name = name;
            return this;
        }

        public Languages WithId(string id)
        {
            Id = id;
            return this;
        }
        public string Name { get; private set; }

        private readonly List<Users> _users = new List<Users>();
        public IReadOnlyList<Users> Users => _users.AsReadOnly();
    }
}