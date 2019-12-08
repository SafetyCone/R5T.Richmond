using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Edenderry;


namespace R5T.Richmond.Construction
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.TryServiceA();
        }

        private static void TryServiceA()
        {
            var serviceProvider = Program.GetServiceProvider();

            var serviceA = serviceProvider.GetRequiredService<IServiceA>();

            var a = serviceA.GetA();

            System.Threading.Thread.Sleep(1000); // Wait for logging message to push out.

            Console.WriteLine($"A in application: {a}");
        }

        private static IServiceProvider GetServiceProvider()
        {
            var serviceProvider = ApplicationBuilder.UseStartup<Startup, ConfigurationStartup>();
            return serviceProvider;
        }
    }
}
