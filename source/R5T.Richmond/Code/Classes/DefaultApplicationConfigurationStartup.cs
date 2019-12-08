using System;

using Microsoft.Extensions.Logging;

namespace R5T.Richmond
{
    /// <summary>
    /// A default application configuration startup.
    /// </summary>
    public class DefaultApplicationConfigurationStartup : ApplicationConfigurationStartupBase
    {
        public DefaultApplicationConfigurationStartup(ILogger<DefaultApplicationConfigurationStartup> logger)
            : base(logger)
        {
        }
    }
}
