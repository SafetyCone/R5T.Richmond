using System;

using Microsoft.Extensions.Logging;

namespace R5T.Richmond
{
    public class DefaultApplicationConfigurationStartup : ApplicationConfigurationStartupBase
    {
        public DefaultApplicationConfigurationStartup(ILogger<DefaultApplicationConfigurationStartup> logger)
            : base(logger)
        {
        }
    }
}
