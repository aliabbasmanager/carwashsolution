using System.Threading.Tasks;

namespace CarWash.StatsViewer
{
    /// <summary>
    /// Statfetcher interface
    /// </summary>
    public interface IStatsFetcher
    {
        /// <summary>
        /// Views the stats on console.
        /// </summary>
        /// <returns></returns>
        Task ViewStatsOnConsole();
    }
}