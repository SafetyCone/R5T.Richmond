using System;

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
    }
}
