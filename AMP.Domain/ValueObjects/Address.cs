using System.Collections.Generic;
using System.Text.Json.Serialization;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects.Base;

namespace AMP.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Countries Country { get; private set; } =
            Countries.Ghana;
        public string City { get; private set; }
        public string Town { get; private set; }
        public string StreetAddress { get; private set; }
        public string StreetAddress2 { get; private set; }

        public Address(){}
        private Address(string city, string address)
        {
            StreetAddress = address;
            City = city;
        }

        /// <summary>
        /// Address = StreetAddress
        /// </summary>
        /// <param name="city"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static Address Create(string city, string address)
        {
            return new Address(city, address);
        }

        public Address FromCountry(Countries country)
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