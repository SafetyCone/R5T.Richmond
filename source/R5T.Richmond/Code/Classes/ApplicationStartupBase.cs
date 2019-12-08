using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace R5T.Richmond
{
    /// <summary>
    /// Base-class for application startup types.
    /// </summary>
    public abstract class ApplicationStartupBase : StartupBase, IApplicationStartup
    {
        public ApplicationStartupBase(ILogger<ApplicationStartupBase> logger)
            : base(logger)
        {
        }

        public void ConfigureConfiguration(IConfigurationBuilder configurationBuilder, IServiceProvider configurationServicesProvider)
        {
            this.Logger.LogDebug("Starting configuration of configuration builder...");

            this.ConfigureConfigurationBody(configurationBuilder, configurationServicesProvider);

            this.Logger.LogDebug("Finished configuration of configuration builder.");
        }

        /// <summary>
        /// Base implementation does nothing.
        /// </summary>
        protected virtual void ConfigureConfigurationBody(IConfigurationBuilder configurationBuilder, IServiceProvider configurationServicesProvider)
        {
            // Do nothing.
        }
    }
}
