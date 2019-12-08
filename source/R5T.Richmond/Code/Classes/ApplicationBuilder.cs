using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Sardinia;


namespace R5T.Richmond
{
    /// <summary>
    /// Static (currently only static, might be worth creating an instantiable type) methods for building an application.
    /// </summary>
    public static class ApplicationBuilder
    {
        public static IServiceProvider UseStartup<TStartup>()
            where TStartup: class, IApplicationStartup
        {
            var serviceProvider = ApplicationBuilder.UseStartup<TStartup, DefaultApplicationConfigurationStartup>();
            return serviceProvider;
        }

        public static IServiceProvider UseStartup<TStartup, TConfigurationStartup>()
            where TStartup: class, IApplicationStartup
            where TConfigurationStartup: class, IApplicationConfigurationStartup
        {
            // Build the standard startup.
            var applicationStartup = ApplicationBuilderInternals.GetApplicationStartup<TStartup>();

            // Configuration.
            var applicationConfigurationBuilder = new ConfigurationBuilder();

            // Get the configuration service provider to use during configuration of the application's configuration builder.
            var configurationServiceProvider = ApplicationBuilderInternals.UseConfigurationStartup<TConfigurationStartup>();

            applicationStartup.ConfigureConfiguration(applicationConfigurationBuilder, configurationServiceProvider);

            var applicationConfiguration = applicationConfigurationBuilder.Build();

            // Configure services.
            var applicationServices = new ServiceCollection();

            applicationServices.AddConfiguration(applicationConfiguration); // Allow the startup class to access its configuration as a service during configuration of services.

            applicationStartup.ConfigureServices(applicationServices);

            var applicationServiceProvider = applicationServices.BuildServiceProvider();

            // Configure service instances.
            applicationStartup.Configure(applicationServiceProvider);

            return applicationServiceProvider;
        }
    }
}
