using CarWash.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Operations.Controllers
{
    /// <summary>
    /// Controller to facilitate statistics sharing
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/Statistics")]
    public class StatisticsController : Controller
    {
        /// <summary>
        /// The operational statistics
        /// </summary>
        private readonly OperationalStatistics operationalStatistics;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsController"/> class.
        /// </summary>
        /// <param name="operationalStatistics">The operational statistics.</param>
        public StatisticsController(OperationalStatistics operationalStatistics)
        {
            this.operationalStatistics = operationalStatistics;
        }

        /// <summary>
        /// Gets the running stats.
        /// </summary>
        /// <returns>The running statistics</returns>
        [HttpGet]
        public OperationalStatistics GetRunningStats()
        {
            return this.operationalStatistics;
        }

    }
}