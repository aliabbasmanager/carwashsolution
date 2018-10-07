using System;
using System.Threading.Tasks;

namespace CarWash.StatsViewer
{
    /// <summary>
    /// Stats fetcher
    /// </summary>
    /// <seealso cref="CarWash.StatsViewer.IStatsFetcher" />
    public class StatsFetcher : IStatsFetcher
    {
        /// <summary>
        /// The HTTP handler
        /// </summary>
        private readonly IHttpHandler httpHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatsFetcher"/> class.
        /// </summary>
        /// <param name="httpHandler">The HTTP handler.</param>
        public StatsFetcher(IHttpHandler httpHandler)
        {
            this.httpHandler = httpHandler;
        }
        /// <summary>
        /// Views the stats on console.
        /// </summary>
        /// <returns></returns>
        public async Task ViewStatsOnConsole()
        {
            while (true)
            {
                var statistics = await httpHandler.GetWashingStatistics();
                Console.WriteLine($"Total Visitors Generated : {statistics.VisitorsGenerated}");
                Console.WriteLine($"Total Cars Accepted : {statistics.VisitorsAccepted}");
                Console.WriteLine($"Total Cars Rejected : {statistics.VisitorsRejected}");
                Console.WriteLine($"Average waiting time : {statistics.AverageTimeSpentInWaiting}");
                Console.WriteLine($"Average washing time : {statistics.AverageTimeSpentInWashing}");
                Console.WriteLine($"Average drying time : {statistics.AverageTimeSpentInDrying}");
                Console.WriteLine("-------------------------------------------------------");
                await Task.Delay(5000);
            }
        }
    }
}
