using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Edenderry;
using R5T.Edenderry.Default;


namespace R5T.Richmond.Construction
{
    public class ConfigurationStartup : ApplicationConfigurationStartupBase
    {
        public ConfigurationStartup(ILogger<ConfigurationStartup> logger)
            : base(logger)
        {
        }

        protected override void ConfigureServicesBody(IServiceCollection services)
        {
            services
                .AddSingleton<IServiceA, ServiceA1>()
                ;
        }
    }
}
