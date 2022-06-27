using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AMP.Domain.ValueObjects.Base;

namespace AMP.Domain.ValueObjects
{
    public class Contact : ValueObject
    {
        [DisplayName("EmailAddress")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; private set; }

        [DisplayName("PrimaryContact")]
        [DataType(DataType.PhoneNumber)]
        public string PrimaryContact { get; private set; }

        [DisplayName("PrimaryContact2")]
        [DataType(DataType.PhoneNumber)]
        public string PrimaryContact2 { get; private set; }

        [DisplayName("PrimaryContact3")]
        [DataType(DataType.PhoneNumber)]
        public string PrimaryContact3 { get; private set; }

        private Contact(string emailAddress, string primaryPhone)
        {
            EmailAddress = emailAddress;
            PrimaryContact = primaryPhone;
        }

        private Contact(){}

        public Contact Create(string emailAddress, string primaryPhone)
        {
            return new Contact(emailAddress, primaryPhone);
        }

        public Contact WithEmailAddress(string emailAddress)
        {
            EmailAddress = emailAddress;
            return this;
        }
        
        public Contact WithPrimaryContact(string primaryContact)
        {
            PrimaryContact = primaryContact;
            return this;
        }
        
        public Contact WithPrimaryContact2(string primaryContact2)
        {
            PrimaryContact2 = primaryContact2;
            return this;
        }

        public Contact WithPrimaryContact3(string primaryContact3)
        {
            PrimaryContact3 = primaryContact3;
            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new System.NotImplementedException();
        }
    }
}