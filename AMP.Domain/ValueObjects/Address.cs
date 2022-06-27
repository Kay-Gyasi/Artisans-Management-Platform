using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects.Base;

namespace AMP.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        [DisplayName("Country")]
        public string Country { get; private set; } =
            CountryType.Countries.FirstOrDefault(x => x.Key == 1).Value;

        [DisplayName("City")]
        public string City { get; private set; }

        [DisplayName("Town")]
        public string Town { get; private set; }

        [DisplayName("StreetAddress")]
        public string StreetAddress { get; private set; }

        [DisplayName("StreetAddress2")]
        public string StreetAddress2 { get; private set; }

        private Address(){}

        private Address(string city, string address)
        {
            StreetAddress = address;
            City = city;
        }

        public Address Create(string city, string address)
        {
            return new Address(city, address);
        }

        public Address FromCountry(string country)
        {
            Country = country;
            return this;
        }

        public Address FromCity(string city)
        {
            City = city;
            return this;
        }

        public Address FromTown(string town)
        {
            Town = town;
            return this;
        }
        
        public Address WithStreetAddress(string streetAddress)
        {
            StreetAddress = streetAddress;
            return this;
        }

        public Address WithStreetAddress2(string streetAddress)
        {
            StreetAddress2 = streetAddress;
            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new System.NotImplementedException();
        }
    }
}