using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Sardinia;

using R5T.Richmond.Extensions;


namespace R5T.Richmond
{
    public static class ApplicationBuilderInternals
    {
        public static TStartup GetApplicationStartup<TStartup>()
            where TStartup : class, IApplicationStartup
        {
            var startupConfiguration = ApplicationBuilderInternals.GetApplicationStartupConfiguration();

            var startupServiceProvider = ApplicationBuilderInternals.GetStartupServiceProvider<TStartup>(startupConfiguration, ApplicationBuilderInternals.DefaultAddLogging);

            var applicationStartup = startupServiceProvider.GetRequiredService<TStartup>();
            return applicationStartup;
        }

        /// <summary>
        /// Gets the configuration startup configuration instance.
        /// Empty configuration.
        /// </summary>
        private static IConfiguration GetApplicationStartupConfiguration()
        {
            var startupConfiguration = new ConfigurationBuilder()
                .Build();

            return startupConfiguration;
        }

        public static IServiceProvider UseConfigurationStartup<TConfigurationStartup>()
            where TConfigurationStartup : class, IApplicationConfigurationStartup
        {
            // Get the configuration startup.
            var configurationStartup = ApplicationBuilderInternals.GetConfigurationStartup<TConfigurationStartup>();

            // Configure the configuration startup.
            var configurationStartupConfigurationBuilder = new ConfigurationBuilder();

            configurationStartup.ConfigureConfiguration(configurationStartupConfigurationBuilder);

            var configurationStartupConfiguration = configurationStartupConfigurationBuilder.Build();

            // Configure services.
            var configurationStartupServices = new ServiceCollection();

            configurationStartupServices.AddConfiguration(configurationStartupConfiguration); // Allow the startup class to access its configuration as a service during configuration of services.

            configurationStartup.ConfigureServices(configurationStartupServices);

            var configurationStartupServiceProvider = configurationStartupServices.BuildServiceProvider();

            // Configure (service instances).
            configurationStartup.Configure(configurationStartupServiceProvider);

            return configurationStartupServiceProvider;
        }

        public static TConfigurationStartup GetConfigurationStartup<TConfigurationStartup>()
            where TConfigurationStartup : class, IApplicationConfigurationStartup
        {
            // Get a configuration startup configuration instance.
            var configurationStartupConfiguration = ApplicationBuilderInternals.GetConfigurationStartupConfiguration();

            // Get the configuration startup service provider.
            var configurationStartupServiceProvider = ApplicationBuilderInternals.GetStartupServiceProvider<TConfigurationStartup>(
                configurationStartupConfiguration,
                ApplicationBuilderInternals.DefaultAddLogging);

            var configurationStartup = configurationStartupServiceProvider.GetRequiredService<TConfigurationStartup>();
            return configurationStartup;
        }

        /// <summary>
        /// Adds console logging.
        /// </summary>
        private static void DefaultAddLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConsole();
        }

        /// <summary>
        /// Gets the configuration startup configuration instance.
        /// Empty configuration.
        /// </summary>
        private static IConfiguration GetConfigurationStartupConfiguration()
        {
            var startupConfiguration = new ConfigurationBuilder()
                .Build();

            return startupConfiguration;
        }

        /// <summary>
        /// Gets a <typeparamref name="TStartup"/> instance.
        /// </summary>
        private static IServiceProvider GetStartupServiceProvider<TStartup>(IConfiguration configuration, Action<ILoggingBuilder> addLogging)
            where TStartup : class, IStartup
        {
            var serviceProvider = new ServiceCollection()
                .AddConfiguration(configuration)
                .AddLogging(addLogging)
                .AddStartup<TStartup>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
