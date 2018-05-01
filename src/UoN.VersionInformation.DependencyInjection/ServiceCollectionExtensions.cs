using Microsoft.Extensions.DependencyInjection;
using System;

namespace UoN.VersionInformation.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddVersionInformation(this IServiceCollection services)
            => services.AddTransient<VersionInformationService>();

        public static void AddVersionInformation(this IServiceCollection services,
            VersionInformationOptions options)
            => services.AddTransient(_ => new VersionInformationService(options));

        public static void AddVersionInformation(
            this IServiceCollection services,
            Action<VersionInformationOptions> configureOptions)
        {
            var options = new VersionInformationOptions();
            configureOptions.Invoke(options);
            services.AddVersionInformation(options);
        }
    }
}
