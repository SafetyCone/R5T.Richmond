using System;

using R5T.Dacia;
using R5T.Tromso.ServiceProvider;
using R5T.Tromso.Startup;


namespace R5T.Richmond
{
    /// <summary>
    /// Basic extensions to the <see cref="ApplicationBuilder"/> type allowing use of a startup type to configure a service provider.
    /// </summary>
    public static class ServiceProviderBuilderExtensions
    {
        /// <summary>
        /// Uses the <typeparamref name="TStartup"/> type configure a service provider instance.
        /// The provided <paramref name="configurationServiceProvider"/> provides services during the configuration configuration process.
        /// </summary>
        public static IServiceProvider UseStartup<TStartup>(this ServiceProviderBuilder serviceProviderBuilder, IServiceProvider configurationServiceProvider)
            where TStartup : class, IStartup
        {
            var serviceProvider = ServiceProvider.New().UseStartup<TStartup, IServiceProvider>(configurationServiceProvider);
            return serviceProvider;
        }

        /// <summary>
        /// Uses the <typeparamref name="TStartup"/> type configure a service provider instance.
        /// The <paramref name="configurationServicesProviderProvider"/> provides a service provider that provides services during the configuration configuration process.
        /// </summary>
        public static IServiceProvider UseStartup<TStartup>(this ServiceProviderBuilder serviceProviderBuilder, Func<IServiceProvider> configurationServicesProviderProvider)
            where TStartup : class, IStartup
        {
            var configurationServicesProvider = configurationServicesProviderProvider();

            var serviceProvider = serviceProviderBuilder.UseStartup<TStartup>(configurationServicesProvider);
            return serviceProvider;
        }

        /// <summary>
        /// Uses the <typeparamref name="TStartup"/> type configure a service provider instance.
        /// An empty service provider is provided during the configuration configuration process.
        /// </summary>
        public static IServiceProvider UseStartup<TStartup>(this ServiceProviderBuilder serviceProviderBuilder)
            where TStartup : class, IStartup
        {
            var serviceProvider = serviceProviderBuilder.UseStartup<TStartup>(ServiceProviderHelper.GetEmptyServiceProvider);
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
        public static IServiceProvider UseStartup<TStartup, TConfigurationStartup>(this ServiceProviderBuilder serviceProviderBuilder, IServiceProvider configurationStartupConfigurationServicesProvider)
                where TStartup : class, IStartup
                where TConfigurationStartup : class, IConfigurationStartup
        {
            var configurationServiceProvider = serviceProviderBuilder.UseStartup<TConfigurationStartup>(configurationStartupConfigurationServicesProvider);

            var serviceProvider = serviceProviderBuilder.UseStartup<TStartup>(configurationServiceProvider);
            return serviceProvider;
        }

        /// <summary>
        /// Uses the <typeparamref name="TStartup"/> type configure a service provider instance.
        /// Uses the <typeparamref name="TConfigurationStartup"/> type to configure a service provider that provides services during the configuration configuration process.
        /// The <paramref name="configurationStartupConfigurationServicesProviderProvider"/> provides a service provider that provides services during the configuration configuration process of the service provider that provides services during the configuration configuration process.
        /// </summary>
        public static IServiceProvider UseStartup<TStartup, TConfigurationStartup>(this ServiceProviderBuilder serviceProviderBuilder, Func<IServiceProvider> configurationStartupConfigurationServicesProviderProvider)
            where TStartup : class, IStartup
            where TConfigurationStartup : class, IConfigurationStartup
        {
            var configurationStartupConfigurationServicesProvider = configurationStartupConfigurationServicesProviderProvider();

            var serviceProvider = serviceProviderBuilder.UseStartup<TStartup, TConfigurationStartup>(configurationStartupConfigurationServicesProvider);
            return serviceProvider;
        }

        /// <summary>
        /// Uses the <typeparamref name="TStartup"/> type configure a service provider instance.
        /// Uses the <typeparamref name="TConfigurationStartup"/> type to configure a service provider that provides services during the configuration configuration process.
        /// An empty service provider is provided during the configuration configuration process of the service provider that provides services during the configuration configuration process.
        /// </summary>
        public static IServiceProvider UseStartup<TStartup, TConfigurationStartup>(this ServiceProviderBuilder serviceProviderBuilder)
            where TStartup : class, IStartup
            where TConfigurationStartup : class, IConfigurationStartup
        {
            var serviceProvider = serviceProviderBuilder.UseStartup<TStartup, TConfigurationStartup>(ServiceProviderHelper.GetEmptyServiceProvider);
            return serviceProvider;
        }

        public static IServiceProvider UseStartup<TStartup, TConfigurationStartup, TConfigurationConfigurationStartup>(this ServiceProviderBuilder serviceProviderBuilder)
            where TStartup : class, IStartup
            where TConfigurationStartup : class, IConfigurationStartup
            where TConfigurationConfigurationStartup: class, IConfigurationStartup
        {
            var configurationConfigurationServiceProvider = serviceProviderBuilder.UseStartup<TConfigurationConfigurationStartup>(ServiceProviderHelper.GetEmptyServiceProvider);

            var serviceProvider = serviceProviderBuilder.UseStartup<TStartup, TConfigurationStartup>(configurationConfigurationServiceProvider);
            return serviceProvider;
        }
    }
}
