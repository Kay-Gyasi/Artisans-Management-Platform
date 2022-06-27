using AMP.Domain.Entities.Base;
using System.Collections.Generic;

namespace AMP.Domain.Entities
{
    public class Services : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        private readonly List<Artisans> _artisans = new List<Artisans>();
        public IEnumerable<Artisans> Artisans => _artisans.AsReadOnly();

        private readonly List<Orders> _orders = new List<Orders>();
        public IEnumerable<Orders> Orders => _orders.AsReadOnly();

        private Services(){}

        private Services(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Services Create(string name, string description)
        {
            return new Services(name, description);
        }

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
    }
}