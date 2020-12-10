using System;

namespace Leeax.Web.Components
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SubComponentAttribute : Attribute
    {
        public SubComponentAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; set; }
    }
}