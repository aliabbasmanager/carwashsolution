using CarWash.VisitorGenerator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace VisitorGenerator
{
    /// <summary>
    /// Program Class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            // Delay the start of visitor generator to have the Api ready
            //Thread.Sleep(2000);

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IGenerateVisitor, GenerateVisitor>()
                .AddTransient<IHttpHandler, HttpHandler>()
                .AddSingleton<IConfiguration>(configuration)
                .BuildServiceProvider();

            serviceProvider.GetService<IGenerateVisitor>().StartGeneration().GetAwaiter().GetResult();
        }
    }
}
