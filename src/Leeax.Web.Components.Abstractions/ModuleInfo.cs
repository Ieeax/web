using System;

namespace Leeax.Web.Components.Abstractions
{
    public readonly struct ModuleInfo
    {
        public ModuleInfo(string path)
            : this(path, null)
        {
        }

        public ModuleInfo(string path, string? name)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Name = name ?? path;
        }

        public string Path { get; }

        public string Name { get; }
    }
}