using System;


namespace R5T.Richmond
{
    public class ServiceProviderBuilder
    {
        #region Static

        public static ServiceProviderBuilder New()
        {
            var applicationBuilder = new ServiceProviderBuilder();
            return applicationBuilder;
        }

        public static IServiceProvider NewUseStartup<TStartup>()
            where TStartup: class, IStartup
        {
            var serviceProvider = ServiceProviderBuilder.New().UseStartup<TStartup>();
            return serviceProvider;
        }

        #endregion
    }
}
