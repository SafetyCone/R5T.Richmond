using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Sardinia;

using R5T.Richmond.Extensions;


namespace R5T.Richmond
{
    public static class ApplicationBuilderHelper
    {
        /// <summary>
        /// Gets an empty startup configuration instance.
        /// </summary>
        public static IConfiguration GetEmptyConfiguration()
        {
            var emptyConfiguration = new ConfigurationBuilder()
                .Build();

            return emptyConfiguration;
        }

        public static IServiceProvider GetEmptyServiceProvider()
        {
            var emptyServiceProvider = new ServiceCollection()
                .BuildServiceProvider();

            return emptyServiceProvider;
        }

        public static TStartup GetStartupInstance<TStartup>()
            where TStartup : class, IApplicationStartup
        {
            var emptyConfiguration = ApplicationBuilderHelper.GetEmptyConfiguration();

            var startupServiceProvider = ApplicationBuilderHelper.GetStartupServiceProvider<TStartup>(emptyConfiguration, ApplicationBuilderHelper.DefaultAddLogging);

            var applicationStartup = startupServiceProvider.GetRequiredService<TStartup>();
            return applicationStartup;
        }

        /// <summary>
        /// Adds console logging.
        /// </summary>
        private static void DefaultAddLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConsole();
        }

        /// <summary>
        /// Gets a service provider that can provide a <typeparamref name="TStartup"/> instance.
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
