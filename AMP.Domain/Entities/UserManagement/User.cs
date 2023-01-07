using System.Collections.Generic;
using AMP.Domain.Entities.Base;
using AMP.Domain.Entities.Messaging;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;

namespace AMP.Domain.Entities.UserManagement
{
    public sealed class User : EntityBase
    {
        public string? ImageId { get; private set; }
        public string FirstName { get; private set; }
        public string FamilyName { get; private set; }
        public string OtherName { get; private set; }
        public string DisplayName { get; private set; }  
        public string MomoNumber { get; private set; }
        public bool IsSuspended { get; private set; }
        public bool IsVerified { get; private set; }
        public bool IsRemoved { get; private set; }
        public UserType Type { get; private set; }
        public LevelOfEducation LevelOfEducation { get; private set; }
        public byte[] Password { get; private set; }
        public byte[] PasswordKey { get; private set; }
        public Contact Contact { get; private set; }
        public Address Address { get; private set; }
        public Image Image { get; private set; }

        private readonly List<Language> _languages = new();
        public IEnumerable<Language> Languages => _languages.AsReadOnly();
        
        private readonly List<Artisan> _artisans = new();
        public IEnumerable<Artisan> Artisans => _artisans.AsReadOnly();

        private readonly List<Customer> _customers = new();
        public IEnumerable<Customer> Customers => _customers.AsReadOnly();
        
        private readonly List<ConnectRequest> _inviterConnectRequests = new();
        public IEnumerable<ConnectRequest> InviterConnectRequests => _inviterConnectRequests.AsReadOnly();

        private readonly List<ConnectRequest> _inviteeConnectRequests = new();
        public IEnumerable<ConnectRequest> InviteeConnectRequests => _inviteeConnectRequests.AsReadOnly();

        private readonly List<Notification> _notifications = new();
        public IEnumerable<Notification> Notifications => _notifications.AsReadOnly();
        
        private readonly List<ChatMessage> _sentMessages = new();
        public IEnumerable<ChatMessage> SentMessages => _sentMessages.AsReadOnly();

        private readonly List<ChatMessage> _receivedMessages = new();
        public IEnumerable<ChatMessage> ReceivedMessages => _receivedMessages.AsReadOnly();

        private readonly List<Conversation> _firstParticipantConvos = new();
        public IEnumerable<Conversation> FirstParticipantConvos => _firstParticipantConvos.AsReadOnly();

        private readonly List<Conversation> _secondParticipantConvos = new();
        public IEnumerable<Conversation> SecondParticipantConvos => _secondParticipantConvos.AsReadOnly();

        private User() {}

        public static User Create() 
            => new User();


        public User WithFirstName(string firstName)
        {
            FirstName = firstName;
            return this;
        }

        public User WithFamilyName(string familyName)
        {
            FamilyName = familyName;
            return this;
        }

        public User WithOtherName(string otherName)
        {
            OtherName = otherName;
            return this;
        }

        public User SetDisplayName()
        {
            DisplayName = string.Join(" ", FirstName.Trim(), FamilyName.Trim());
            return this;
        }

        public User WithImageId(string id)
        {
            ImageId = id;
            return this;
        }
        
        public User WithImage(Image image)
        {
            Image = image;
            return this;
        }

        public User OfType(UserType type)
        {
            Type = type;
            return this;
        }

        public User HasLevelOfEducation(LevelOfEducation level)
        {
            LevelOfEducation = level;
            return this;
        }

        public User WithContact(Contact contact)
        {
            Contact = contact;
            return this;
        }

        public User WithAddress(Address address)
        {
            Address = address;
            return this;
        }

        public User Speaks(IEnumerable<Language> languages)
        {
            _languages.Clear();
            _languages.AddRange(languages);
            return this;
        }

        public User WithMomoNumber(string number)
        {
            MomoNumber = number;
            return this;
        }

        public User IsSuspendedd(bool isSuspended)
        {
            IsSuspended = isSuspended;
            return this;
        }

        public User IsRemovedd(bool isRemoved)
        {
            IsRemoved = isRemoved;
            return this;
        }

        public User HasPassword(byte[] hash)
        {
            Password = hash;
            return this;
        }

        public User HasPasswordKey(byte[] hash)
        {
            PasswordKey = hash;
            return this;
        }

        public User WithId(string id)
        {
            Id = id;
            return this;
        }
        
        public User Verify()
        {
            IsVerified = true;
            return this;
        }
    }
}