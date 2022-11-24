using System.Collections.Generic;
using AMP.Domain.ValueObjects.Base;

namespace AMP.Domain.ValueObjects
{
    public class Contact : ValueObject
    {
        public string EmailAddress { get; private set; }
        public string PrimaryContact { get; private set; }
        public string PrimaryContact2 { get; private set; }
        public string PrimaryContact3 { get; private set; }

        private Contact(string primaryPhone)
        {
            PrimaryContact = primaryPhone;
        }

        private Contact(){}

        public static Contact Create(string primaryPhone)
        {
            return new Contact(primaryPhone);
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