using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarWash.VisitorGenerator
{
    /// <summary>
    /// Visitor Generator
    /// </summary>
    /// <seealso cref="CarWash.VisitorGenerator.IGenerateVisitor" />
    class GenerateVisitor : IGenerateVisitor
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// The HTTP handler
        /// </summary>
        private readonly IHttpHandler httpHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateVisitor"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="httpHandler">The HTTP handler.</param>
        public GenerateVisitor(IConfiguration configuration, IHttpHandler httpHandler)
        {
            this.configuration = configuration;
            this.httpHandler = httpHandler;
        }

        /// <summary>
        /// Starts the generation.
        /// </summary>
        /// <returns></returns>
        public async Task StartGeneration()
        {
            var intervalUpperLimit = Convert.ToInt16(configuration["VisitorGenerationIntervalInSecondsUpperLimit"]);
            while (true)
            {
                var random = new Random();
                int value = random.Next(0, intervalUpperLimit); 
                var guid = Guid.NewGuid();
                Console.WriteLine($"User generated. Next generation will happen after {value} seconds");
                var status = await httpHandler.sendVisitorToQueue(guid);
                Console.WriteLine(status);
                
                Thread.Sleep(value * 1000);
            }
        }
    }
}
