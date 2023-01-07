using System.Collections.Generic;
using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities.UserManagement
{
    public sealed class Language : EntityBase
    {
        private Language(string name)
        {
            Name = name;
        }

        public static Language Create(string name) 
            => new Language(name);
        
        public string Name { get; private set; }

        private readonly List<User> _users = new();
        public IReadOnlyList<User> Users => _users.AsReadOnly();
    }
}