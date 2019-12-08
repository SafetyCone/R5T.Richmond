using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace R5T.Richmond
{
    public class ApplicationConfigurationStartupBase : StartupBase, IApplicationConfigurationStartup
    {
        public ApplicationConfigurationStartupBase(ILogger<ApplicationConfigurationStartupBase> logger)
            : base(logger)
        {
        }

        public void ConfigureConfiguration(IConfigurationBuilder configurationBuilder)
        {
            this.Logger.LogDebug("Starting configuration of application configuration configuration builder...");

            this.ConfigureConfigurationBody(configurationBuilder);

            this.Logger.LogDebug("Finished configuration of application configuration configuration builder.");
        }

        /// <summary>
        /// Base implementation does nothing.
        /// </summary>
        protected virtual void ConfigureConfigurationBody(IConfigurationBuilder configurationBuilder)
        {
            // Do nothing.
        }
    }
}
