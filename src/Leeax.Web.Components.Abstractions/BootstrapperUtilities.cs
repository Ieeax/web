using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Abstractions
{
    public static class BootstrapperUtilities
    {
        public static async Task RunFromServiceProviderAsync(IServiceProvider serviceProvider)
        {
            var bootstrappers = serviceProvider.GetServices<IBootstrapper>();

            foreach (var curBootstrapper in bootstrappers)
            {
                // Skip invalid bootstrapper
                if (curBootstrapper == null)
                {
                    continue;
                }

                var name = curBootstrapper.Name ?? "Unnamed";

                try
                {
                    // Run the bootstrapper and wait for the exit code
                    var exitCode = await curBootstrapper.RunAsync(serviceProvider);

                    // Everything other than zero is a possible failure
                    // -> Log some info into console (we may want to integrate an actual logger at some point)
                    if (exitCode != 0)
                    {
                        Console.WriteLine($"[{curBootstrapper.GetType().FullName}] Bootstrapper \"{name}\" exited with code \"{exitCode}\".");
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception into the console
                    Console.WriteLine($"[{curBootstrapper.GetType().FullName}] Bootstrapper \"{name}\" exited unexpected with code \"1\": " + ex.Message);
                    throw;
                }
            }
        }
    }
}