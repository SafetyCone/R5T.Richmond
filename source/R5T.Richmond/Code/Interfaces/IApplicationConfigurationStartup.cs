using System;

using Microsoft.Extensions.Configuration;


namespace R5T.Richmond
{
    /// <summary>
    /// Describes the startup for the first-stage of a two-stage dependency-injection container application startup process.
    /// Two-stage application startup is required since a service-provider might be needed during configuration of the configuration builder for the second-stage, actual application startup.
    /// </summary>
    public interface IApplicationConfigurationStartup : IStartup
    {
        void ConfigureConfiguration(IConfigurationBuilder configurationBuilder);
    }
}
