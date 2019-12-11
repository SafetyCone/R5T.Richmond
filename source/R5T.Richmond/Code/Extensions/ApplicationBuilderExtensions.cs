using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Sardinia;


namespace R5T.Richmond
{
    /// <summary>
    /// Basic extensions to the <see cref="ApplicationBuilder"/> type allowing use of a startup type to configure a service provider.
    /// </summary>
    public static class BasicApplicationBuilderExtensions
    {
        /// <summary>
        /// Uses the <typeparamref name="TStartup"/> type configure a service provider instance.
        /// The provided <paramref name="configurationServiceProvider"/> provides services during the configuration configuration process.
        /// </summary>
        public static IServiceProvider UseStartup<TStartup>(this ApplicationBuilder applicationBuilder, IServiceProvider configurationServiceProvider)
            where TStartup : class, IApplicationStartup
        {
            // Build the standard startup.
            var applicationStartup = ApplicationBuilderHelper.GetStartupInstance<TStartup>();

            // Configuration.
            var applicationConfigurationBuilder = new ConfigurationBuilder();

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

        /// <summary>
        /// Uses the <typeparamref name="TStartup"/> type configure a service provider instance.
        /// The <paramref name="configurationServicesProviderProvider"/> provides a service provider that provides services during the configuration configuration process.
        /// </summary>
        public static IServiceProvider UseStartup<TStartup>(this ApplicationBuilder applicationBuilder, Func<IServiceProvider> configurationServicesProviderProvider)
            where TStartup : class, IApplicationStartup
        {
            var configurationServicesProvider = configurationServicesProviderProvider();

            var serviceProvider = applicationBuilder.UseStartup<TStartup>(configurationServicesProvider);
            return serviceProvider;
        }

        /// <summary>
        /// Uses the <typeparamref name="TStartup"/> type configure a service provider instance.
        /// An empty service provider is provided during the configuration configuration process.
        /// </summary>
        public static IServiceProvider UseStartup<TStartup>(this ApplicationBuilder applicationBuilder)
            where TStartup : class, IApplicationStartup
        {
            var serviceProvider = applicationBuilder.UseStartup<TStartup>(ApplicationBuilderHelper.GetEmptyServiceProvider);
            return serviceProvider;
        }
    }

    /// <summary>
    /// Extensions to the <see cref="ApplicationBuilder"/> type allowing use of a configuration startup type that configures a service provider that provides services during configuration configuration.
    /// </summary>
    public static class ConfigurationTypeApplicationBuilderExtensions
    {
        /// <summary>
        /// Uses the <typeparamref name="TStartup"/> type configure a service provider instance.
        /// Uses the <typeparamref name="TConfigurationStartup"/> type to configure a service provider that provides services during the configuration configuration process.
        /// The <paramref name="configurationStartupConfigurationServicesProvider"/> provides services during the configuration configuration process of the service provider that provides services during the configuration configuration process.
        /// </summary>
        public static IServiceProvider UseStartup<TStartup, TConfigurationStartup>(this ApplicationBuilder applicationBuilder, IServiceProvider configurationStartupConfigurationServicesProvider)
                where TStartup : class, IApplicationStartup
                where TConfigurationStartup : class, IApplicationConfigurationStartup
        {
            var configurationServiceProvider = applicationBuilder.UseStartup<TConfigurationStartup>(configurationStartupConfigurationServicesProvider);

            var serviceProvider = applicationBuilder.UseStartup<TStartup>(configurationServiceProvider);
            return serviceProvider;
        }

        /// <summary>
        /// Uses the <typeparamref name="TStartup"/> type configure a service provider instance.
        /// Uses the <typeparamref name="TConfigurationStartup"/> type to configure a service provider that provides services during the configuration configuration process.
        /// The <paramref name="configurationStartupConfigurationServicesProviderProvider"/> provides a service provider that provides services during the configuration configuration process of the service provider that provides services during the configuration configuration process.
        /// </summary>
        public static IServiceProvider UseStartup<TStartup, TConfigurationStartup>(this ApplicationBuilder applicationBuilder, Func<IServiceProvider> configurationStartupConfigurationServicesProviderProvider)
            where TStartup : class, IApplicationStartup
            where TConfigurationStartup : class, IApplicationConfigurationStartup
        {
            var configurationStartupConfigurationServicesProvider = configurationStartupConfigurationServicesProviderProvider();

            var serviceProvider = applicationBuilder.UseStartup<TStartup, TConfigurationStartup>(configurationStartupConfigurationServicesProvider);
            return serviceProvider;
        }

        /// <summary>
        /// Uses the <typeparamref name="TStartup"/> type configure a service provider instance.
        /// Uses the <typeparamref name="TConfigurationStartup"/> type to configure a service provider that provides services during the configuration configuration process.
        /// An empty service provider is provided during the configuration configuration process of the service provider that provides services during the configuration configuration process.
        /// </summary>
        public static IServiceProvider UseStartup<TStartup, TConfigurationStartup>(this ApplicationBuilder applicationBuilder)
            where TStartup : class, IApplicationStartup
            where TConfigurationStartup : class, IApplicationConfigurationStartup
        {
            var serviceProvider = applicationBuilder.UseStartup<TStartup, TConfigurationStartup>(ApplicationBuilderHelper.GetEmptyServiceProvider);
            return serviceProvider;
        }
    }
}
