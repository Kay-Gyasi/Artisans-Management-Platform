using System.Collections.Generic;
using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities
{
    public class Languages : EntityBase
    {
        public string Name { get; private set; }

        private Languages(){}

        private Languages(string name)
        {
            Name = name;
        }

        public Languages Create(string name)
        {
            return new Languages(name);
        }

        public Languages WithName(string name)
        {
            Name = name;
            return this;
        }
    }
}