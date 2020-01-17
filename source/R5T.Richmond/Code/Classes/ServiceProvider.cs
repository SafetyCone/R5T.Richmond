using System;

using R5T.Tromso;
using R5T.Tromso.Types;


namespace R5T.Richmond
{
    public static class ServiceProvider
    {
        public static IServiceBuilder New()
        {
            var serviceProviderHelper = new ServiceBuilder();
            return serviceProviderHelper;
        }
    }
}
