using System.Collections.Generic;
using AMP.Domain.Entities.Base;
using AMP.Domain.Entities.UserManagement;

namespace AMP.Domain.Entities.BusinessManagement
{
    public sealed class Service : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        private readonly List<Artisan> _artisans = new();
        public IEnumerable<Artisan> Artisans => _artisans.AsReadOnly();

        private readonly List<Order> _orders = new();
        public IEnumerable<Order> Orders => _orders.AsReadOnly();

        private Service(){}

        private Service(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static Service Create(string name, string description = "") 
            => new Service(name, description);

        public Service WithName(string name)
        {
            Name = name;
            return this;
        }

        public Service WithDescription(string description)
        {
            Description = description;
            return this;
        }
    }
}