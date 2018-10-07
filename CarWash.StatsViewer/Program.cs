using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading;

namespace CarWash.StatsViewer
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
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IStatsFetcher, StatsFetcher>()
                .AddTransient<IHttpHandler, HttpHandler>()
                .AddSingleton<IConfiguration>(configuration)
                .BuildServiceProvider();

            // Delay the start of stat collector to let the wasahing process kick in
            Thread.Sleep(Convert.ToInt16(configuration["WashingStartWaitTime"]) * 1000);

            serviceProvider.GetService<IStatsFetcher>().ViewStatsOnConsole().GetAwaiter().GetResult();
        }
    }
}
