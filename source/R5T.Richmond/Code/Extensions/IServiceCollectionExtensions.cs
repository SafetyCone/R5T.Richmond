using System;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.Richmond.Extensions
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the startup class type in the standard way; as a service-type-is-implementation-type singleton.
        /// </summary>
        public static IServiceCollection AddStartup<TStartup>(this IServiceCollection services)
        where TStartup : class, IStartup
        {
            services
                .AddSingleton<TStartup>()
                ;

            return services;
        }
    }
}
