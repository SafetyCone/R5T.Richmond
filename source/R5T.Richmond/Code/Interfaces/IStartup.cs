using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace R5T.Richmond
{
    /// <summary>
    /// Describes a startup type that configures a configuration builder, a services collection, and a service provider.
    /// </summary>
    public interface IStartup
    {
        void ConfigureServices(IServiceCollection services);
        void Configure(IServiceProvider serviceProvider);
    }
}
