using System;
using AMP.Domain.Entities.Base;
using System.Collections.Generic;

namespace AMP.Domain.Entities
{
    public sealed class Services : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        private readonly List<Artisans> _artisans = new();
        public IEnumerable<Artisans> Artisans => _artisans.AsReadOnly();

        private readonly List<Orders> _orders = new();
        public IEnumerable<Orders> Orders => _orders.AsReadOnly();

        private Services(){}

        private Services(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static Services Create(string name, string description = "") 
            => new Services(name, description);

        public Services WithName(string name)
        {
            Name = name;
            return this;
        }

        public Services WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public Services CreatedOn()
        {
            DateCreated = DateTime.UtcNow;
            return this;
        }

        public Services LastModifiedOn()
        {
            DateModified = DateTime.UtcNow;
            return this;
        }
    }
}