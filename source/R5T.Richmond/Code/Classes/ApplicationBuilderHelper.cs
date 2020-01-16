using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Chamavia;
using R5T.Sardinia;

using R5T.Richmond.Extensions;


namespace R5T.Richmond
{
    public static class ApplicationBuilderHelper
    {
        public static TStartup GetStartupInstance<TStartup>()
            where TStartup : class, IApplicationStartup
        {
            var emptyConfiguration = ConfigurationHelper.GetEmptyConfiguration();

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
