using System;

namespace ComponentsDemo.Components
{
    public class CascadingParameterInfo
    {
        public CascadingParameterInfo(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; set; }

        public Type Type { get; set; }
    }
}
