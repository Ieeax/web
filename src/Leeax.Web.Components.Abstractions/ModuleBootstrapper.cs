using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Abstractions
{
    /// <summary>
    /// Imports a javascript ES6 module and stores it in the registered <see cref="IJSObjectReferenceStore"/>.
    /// </summary>
    public class ModuleBootstrapper : IBootstrapper
    {
        private readonly IEnumerable<ModuleInfo> _modules;

        public ModuleBootstrapper(ModuleInfo module)
        {
            _modules = new ModuleInfo[] { module };
        }

        public ModuleBootstrapper(IEnumerable<ModuleInfo> modules)
        {
            _modules = modules ?? throw new ArgumentNullException(nameof(modules));
        }

        public async Task<int> RunAsync(IServiceProvider serviceProvider)
        {
            var jsRuntime = serviceProvider.GetRequiredService<IJSRuntime>();
            var store = serviceProvider.GetRequiredService<IJSObjectReferenceStore>();

            foreach (var curModule in _modules)
            {
                if (jsRuntime is IJSInProcessRuntime)
                {
                    await jsRuntime.ImportModuleAsync<IJSInProcessObjectReference>(
                        curModule.Path,
                        curModule.Name,
                        store);
                }
                else
                {
                    await jsRuntime.ImportModuleAsync(
                        curModule.Path,
                        curModule.Name,
                        store);
                }
            }

            return 0;
        }

        public string Name => "ES6 module bootstrapper";
    }
}