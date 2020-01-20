using System;

using Microsoft.Extensions.Logging;


namespace R5T.Richmond
{
    public class ApplicationConfigurationStartupBase : StartupBase, IConfigurationStartup
    {
        public ApplicationConfigurationStartupBase(ILogger<ApplicationConfigurationStartupBase> logger)
            : base(logger)
        {
        }
    }
}
