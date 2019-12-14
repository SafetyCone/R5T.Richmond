using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace R5T.Richmond
{
    /// <summary>
    /// Base-class for all startup types.
    /// </summary>
    public abstract class StartupBase : IStartup
    {
        protected ILogger Logger { get; }


        public StartupBase(ILogger<StartupBase> logger)
        {
            this.Logger = logger;
        }

        public void ConfigureConfiguration(IConfigurationBuilder configurationBuilder, IServiceProvider configurationServiceProvider)
        {
            this.Logger.LogDebug("Starting configuration of configuration builder...");

            this.ConfigureConfigurationBody(configurationBuilder, configurationServiceProvider);

            this.Logger.LogDebug("Finished configuration of configuration builder.");
        }

        /// <summary>
        /// Base implementation does nothing.
        /// </summary>
        protected virtual void ConfigureConfigurationBody(IConfigurationBuilder configurationBuilder, IServiceProvider configurationServiceProvider)
        {
            // Do nothing.
        }

        public void ConfigureServices(IServiceCollection services)
        {
            this.Logger.LogDebug("Starting configuration of service collection...");

            this.ConfigureServicesBody(services);

            this.Logger.LogDebug("Finished configuration of service collection.");
        }

        /// <summary>
        /// Base implementation does nothing.
        /// </summary>
        protected virtual void ConfigureServicesBody(IServiceCollection services)
        {
            // Do nothing.
        }

        public void Configure(IServiceProvider serviceProvider)
        {
            this.Logger.LogDebug("Starting configure of service provider...");

            this.ConfigureBody(serviceProvider);

            this.Logger.LogDebug("Finished configure of service provider.");
        }

        /// <summary>
        /// Base implementation does nothing.
        /// </summary>
        protected virtual void ConfigureBody(IServiceProvider serviceProvider)
        {
            // Do nothing.
        }
    }
}
