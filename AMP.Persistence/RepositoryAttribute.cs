using System;

namespace AMP.Persistence
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RepositoryAttribute : Attribute
    {
        // For identifying and registering repositories
    }
}