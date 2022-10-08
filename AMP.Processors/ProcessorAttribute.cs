using System;

namespace AMP.Processors
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ProcessorAttribute : Attribute
    {
        // Used for identifying and registering processors
    }
}