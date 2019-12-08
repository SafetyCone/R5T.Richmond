using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Edenderry;
using R5T.Edenderry.Default;


namespace R5T.Richmond.Construction
{
    public class Startup : ApplicationStartupBase
    {
        public Startup(ILogger<Startup> logger)
            : base(logger)
        {
        }

        protected override void ConfigureConfigurationBody(IConfigurationBuilder configurationBuilder, IServiceProvider configurationServicesProvider)
        {
            var serviceA = configurationServicesProvider.GetRequiredService<IServiceA>();

            var a = serviceA.GetA();

            this.Logger.LogInformation($"A in configure configuration: {a}");
        }

        protected override void ConfigureServicesBody(IServiceCollection services)
        {
            services
                .AddSingleton<IServiceA, ServiceA2>()
                ;
        }
    }
}
