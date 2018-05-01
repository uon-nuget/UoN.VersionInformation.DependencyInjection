using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UoN.VersionInformation;
using UoN.VersionInformation.DependencyInjection;
using UoN.VersionInformation.Providers;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Default options
            var services = ConfigureServices_NoOptions(new ServiceCollection());

            var version = services.GetRequiredService<VersionInformationService>();

            Console.WriteLine(await version.EntryAssemblyAsync());

            // Options object
            services = ConfigureServices_OptionsObject(new ServiceCollection());

            version = services.GetRequiredService<VersionInformationService>();

            Console.WriteLine(await version.ByKeyAsync("Assembly"));

            // Options Delegate
            services = ConfigureServices_OptionsDelegate(new ServiceCollection());

            version = services.GetRequiredService<VersionInformationService>();

            Console.WriteLine(await version.ByKeyAsync("Assembly"));

            Console.ReadKey();
        }

        private static IServiceProvider ConfigureServices_NoOptions(IServiceCollection services)
        {
            services.AddVersionInformation();
            return services.BuildServiceProvider();
        }

        private static IServiceProvider ConfigureServices_OptionsObject(IServiceCollection services)
        {
            var options = new VersionInformationOptions
            {
                KeyHandlers = new Dictionary<string, IVersionInformationProvider>
                {
                    ["Assembly"] = new AssemblyInformationalVersionProvider()
                }
            };

            services.AddVersionInformation(options);
            return services.BuildServiceProvider();
        }

        private static IServiceProvider ConfigureServices_OptionsDelegate(IServiceCollection services)
        {
            services.AddVersionInformation(opts =>
            {
                opts.KeyHandlers.Add("Assembly", new AssemblyInformationalVersionProvider());
            });
            return services.BuildServiceProvider();
        }
    }
}
