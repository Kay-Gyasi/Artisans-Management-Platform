using System;
using System.Collections.Generic;
using System.Linq;
using AMP.Domain.Entities.Base;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;

namespace AMP.Domain.Entities
{
    public class Users : EntityBase
    {
        
        public string FirstName { get; private set; }
        public string FamilyName { get; private set; }
        public string OtherName { get; private set; }
        public string DisplayName { get; private set; }  
        public string ImageUrl { get; private set; }
        public string MomoNumber { get; private set; }
        public bool IsSuspended { get; private set; }
        public bool IsRemoved { get; private set; }
        public UserType Type { get; private set; }
        public LevelOfEducation LevelOfEducation { get; private set; }
        public byte[] Password { get; private set; }
        public byte[] PasswordKey { get; private set; }
        public Contact Contact { get; private set; }
        public Address Address { get; private set; }

        private readonly List<Languages> _languages = new List<Languages>();
        public IEnumerable<Languages> Languages => _languages.AsReadOnly();
        
        private readonly List<Artisans> _artisans = new List<Artisans>();
        public IEnumerable<Artisans> Artisans => _artisans.AsReadOnly();

        private readonly List<Customers> _customers = new List<Customers>();
        public IEnumerable<Customers> Customers => _customers.AsReadOnly();

        private Users() {}

        public static Users Create()
        {
            return new Users();
        }


        public Users WithFirstName(string firstName)
        {
            FirstName = firstName;
            return this;
        }

        public Users WithFamilyName(string familyName)
        {
            FamilyName = familyName;
            return this;
        }

        public Users WithOtherName(string otherName)
        {
            OtherName = otherName;
            return this;
        }

        public Users SetDisplayName()
        {
            DisplayName = FirstName.Trim() + " " + FamilyName.Trim();
            return this;
        }

        public Users WithImageUrl(string imageUrl)
        {
            ImageUrl = imageUrl;
            return this;
        }

        public Users OfType(UserType type)
        {
            Type = type;
            return this;
        }

        public Users HasLevelOfEducation(LevelOfEducation level)
        {
            LevelOfEducation = level;
            return this;
        }

        public Users WithContact(Contact contact)
        {
            Contact = contact;
            return this;
        }

        public Users WithAddress(Address address)
        {
            Address = address;
            return this;
        }

        public Users Speaks(List<Languages> languages)
        {
            _languages.Clear();
            _languages.AddRange(languages);
            return this;
        }

        public Users WithMomoNumber(string number)
        {
            MomoNumber = number;
            return this;
        }

        public Users IsSuspendedd(bool isSuspended)
        {
            IsSuspended = isSuspended;
            return this;
        }

        public Users IsRemovedd(bool isRemoved)
        {
            IsRemoved = isRemoved;
            return this;
        }

        public Users CreatedOn(DateTime date)
        {
            DateCreated = date;
            return this;
        }

        public Users HasPassword(byte[] hash)
        {
            Password = hash;
            return this;
        }

        public Users HasPasswordKey(byte[] hash)
        {
            PasswordKey = hash;
            return this;
        }
    }
}