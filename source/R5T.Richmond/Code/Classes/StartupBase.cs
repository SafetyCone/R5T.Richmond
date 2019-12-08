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
