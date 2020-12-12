using System;

namespace ComponentsDemo.Components
{
    public class ParameterInfo
    {
        public ParameterInfo(string name, Type type, bool captureUnmatchedValues)
        {
            Name = name;
            Type = type;
            CaptureUnmatchedValues = captureUnmatchedValues;
        }

        public string Name { get; set; }

        public Type Type { get; set; }

        public bool CaptureUnmatchedValues { get; set; }
    }
}
