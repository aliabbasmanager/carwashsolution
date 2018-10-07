using CarWash.Core.Entities;
using System.Threading.Tasks;

namespace CarWash.StatsViewer
{
    /// <summary>
    /// HttpHandler interface
    /// </summary>
    public interface IHttpHandler
    {
        /// <summary>
        /// Gets the washing statistics.
        /// </summary>
        /// <returns></returns>
        Task<OperationalStatistics> GetWashingStatistics();
    }
}