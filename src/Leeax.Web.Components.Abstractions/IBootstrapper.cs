using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Abstractions
{
    public interface IBootstrapper
    {
        /// <summary>
        /// Runs the bootstrapper and returns an exit code.
        /// Everything other than zero is considered as a failure.
        /// </summary>
        public Task<int> RunAsync(IServiceProvider serviceProvider);

        /// <summary>
        /// Gets the concrete name of the bootstrapper.
        /// </summary>
        string Name { get; }
    }
}