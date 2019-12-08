using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Sardinia;

using R5T.Richmond.Extensions;


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
            var applicationStartup = ApplicationBuilder.GetApplicationStartup<TStartup>();

            // Configuration.
            var applicationConfigurationBuilder = new ConfigurationBuilder();

            // Get the configuration service provider to use during configuration of the application's configuration builder.
            var configurationServiceProvider = ApplicationBuilder.UseConfigurationStartup<TConfigurationStartup>();

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

        public static TStartup GetApplicationStartup<TStartup>()
            where TStartup : class, IApplicationStartup
        {
            var startupConfiguration = ApplicationBuilder.GetApplicationStartupConfiguration();

            var startupServiceProvider = ApplicationBuilder.GetStartupServiceProvider<TStartup>(startupConfiguration, ApplicationBuilder.DefaultAddLogging);

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
            var configurationStartup = ApplicationBuilder.GetConfigurationStartup<TConfigurationStartup>();

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
            where TConfigurationStartup: class, IApplicationConfigurationStartup
        {
            // Get a configuration startup configuration instance.
            var configurationStartupConfiguration = ApplicationBuilder.GetConfigurationStartupConfiguration();

            // Get the configuration startup service provider.
            var configurationStartupServiceProvider = ApplicationBuilder.GetStartupServiceProvider<TConfigurationStartup>(configurationStartupConfiguration, ApplicationBuilder.DefaultAddLogging);

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
