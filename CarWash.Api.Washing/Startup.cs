using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CarWash.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarWash.Api.Operations
{
    /// <summary>
    /// Statrtup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<QueueCollection>();
            services.AddSingleton<OperationalStatistics>();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var queueCollection = app.ApplicationServices.GetService<QueueCollection>();
            var configuration = app.ApplicationServices.GetService<IConfiguration>();
            var operationalStatistics = app.ApplicationServices.GetService<OperationalStatistics>();

            Action<int> ExecuteWashing = async (int x) =>
            {
                while (true)
                {
                    if (queueCollection.WashingQueue[x].TryDequeue(out var visitor))
                    {
                        operationalStatistics.TotalTimeSpentInWaiting += (DateTime.UtcNow - visitor.EntryTimeInQueue).TotalSeconds;

                        Stopwatch stopWatch = new Stopwatch();
                        stopWatch.Start();
                        Random r = new Random();
                        var deviation = r.Next(-2, 2);

                        var processTime = Convert.ToInt16(configuration["WashingMeanTime"]) + deviation;
                        await Task.Delay(processTime * 1000);
                        
                        var dryingQueueSize = Convert.ToInt16(configuration["DryingQueueSize"]);

                        while(queueCollection.DryingQueue.Count == dryingQueueSize)
                        {
                            //Wait for the a spot to open in the drying queue
                            await Task.Delay(100);
                        }

                        stopWatch.Stop();
                        // Updating the Stats for every car washed
                        operationalStatistics.TotalTimeSpentInWashing += stopWatch.ElapsedMilliseconds / 1000;
                        operationalStatistics.TotalNumberOfCarsWashed++;

                        // Add the visitor to the drying queue.
                        // ConcurrentQueue type will take care of additions in the Queue if they happen simultaneously
                        visitor.EntryTimeInQueue = DateTime.UtcNow;
                        queueCollection.DryingQueue.Enqueue(visitor);
                    }
                }
            };

            Action ExecuteDrying = async () =>
            {
                // Drying process should only start when atleast one washing is complete. 
                // Start the actual execution of this thread only after minimum time required to wash a car has elapsed.
                var minWashingProcessTime = Convert.ToInt16(configuration["WashingMeanTime"]) - 2;
                Thread.Sleep(minWashingProcessTime);

                while (true)
                {
                    if (queueCollection.DryingQueue.TryDequeue(out var visitor))
                    {
                        operationalStatistics.TotalTimeSpentInWaiting += (DateTime.UtcNow - visitor.EntryTimeInQueue).TotalSeconds;

                        Stopwatch stopWatch = new Stopwatch();
                        stopWatch.Start();
                        Random r = new Random();
                        var deviation = r.Next(-2, 2);

                        var processTime = Convert.ToInt16(configuration["DryingMeanTime"]) + deviation;
                        await Task.Delay(processTime * 1000);
                        stopWatch.Stop();

                        operationalStatistics.TotalTimeSpentInDrying += stopWatch.ElapsedMilliseconds / 1000;
                        operationalStatistics.TotalNumberOfCarsDried++;
                    }
                }
            };

            // Start washing background process
            Task.Run(() => ExecuteWashing(0));
            Task.Run(() => ExecuteWashing(1));
            Task.Run(() => ExecuteWashing(2));

            // Start Drying background process
            Task.Run(() => ExecuteDrying());
            Task.Run(() => ExecuteDrying());

            app.UseMvc();
        }
    }
}
