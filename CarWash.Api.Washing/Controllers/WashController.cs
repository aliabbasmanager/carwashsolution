using CarWash.Api.Washing.ResponseModel;
using CarWash.Core.Entities;
using CarWash.Core.Entities.RequestModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace CarWash.Api.Operations.Controllers
{
    /// <summary>
    /// Controller to handle Washing related requests
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class WashController : Controller
    {
        /// <summary>
        /// The queue collection
        /// </summary>
        private readonly QueueCollection queueCollection;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// The operational statistics
        /// </summary>
        private readonly OperationalStatistics operationalStatistics;

        /// <summary>
        /// Initializes a new instance of the <see cref="WashController"/> class.
        /// </summary>
        /// <param name="queueCollection">The queue collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="operationalStatistics">The operational statistics.</param>
        public WashController(QueueCollection queueCollection, IConfiguration configuration, OperationalStatistics operationalStatistics)
        {
            this.queueCollection = queueCollection;
            this.configuration = configuration;
            this.operationalStatistics = operationalStatistics;
        }

        // PUT api/values/5
        /// <summary>
        /// Puts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Operation response</returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]User value)
        {
            var queueIndex = this.getMostVacantQueueIndex();
            var queueSize = Convert.ToInt16(this.configuration["WashingQueueSize"]);
            operationalStatistics.VisitorsGenerated += 1;
           
            // Check if the visitor can be accepted in any of the queues.
            if (this.queueCollection.WashingQueue[queueIndex].Count < queueSize)
            {
                // We track the time at which each visitor enters the queue so that we can calculate the waiting time when s/he is dequeued
                var visitor = new Visitor()
                {
                    VisitorIdentifier = new Guid(value.Id),
                    EntryTimeInQueue = DateTime.UtcNow
                };

                this.queueCollection.WashingQueue[queueIndex].Enqueue(visitor);
                operationalStatistics.VisitorsAccepted++;
                return this.Ok(new VisitorRequestResponse() { statusCode = "201", Message = $"Visitor {value.Id} added to the washing queue {queueIndex + 1}" });
            }
            else
            {
                operationalStatistics.VisitorsRejected++;
                return this.Ok(new VisitorRequestResponse() { statusCode = "403", Message = $"Queue full. Visitor {value.Id} rejected" });
            }
        }

        /// <summary>
        /// Gets the index of the most vacant queue.
        /// </summary>
        /// <returns>The queue index which is most vacant</returns>
        private int getMostVacantQueueIndex()
        {
            var queueCount1 = this.queueCollection.WashingQueue[0].Count;
            var queueCount2 = this.queueCollection.WashingQueue[1].Count;
            var queueCount3 = this.queueCollection.WashingQueue[2].Count;

            if (queueCount1 <= queueCount2 && queueCount1 <= queueCount3)
                return 0;

            else if (queueCount2 <= queueCount1 && queueCount2 <= queueCount3)
                return 1;

            else if (queueCount3 <= queueCount2 && queueCount3 <= queueCount1)
                return 2;

            else
                return 0;
        }
    }
}
