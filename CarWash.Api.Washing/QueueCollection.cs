using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CarWash.Api.Operations
{
    /// <summary>
    /// Entity to have all the queues
    /// </summary>
    public class QueueCollection
    {
        /// <summary>
        /// The washing queue
        /// </summary>
        public readonly IList<ConcurrentQueue<Visitor>> WashingQueue = new List<ConcurrentQueue<Visitor>>(3)
        {
            new ConcurrentQueue<Visitor>(),
            new ConcurrentQueue<Visitor>(),
            new ConcurrentQueue<Visitor>()
        };

        /// <summary>
        /// The drying queue
        /// </summary>
        public readonly ConcurrentQueue<Visitor> DryingQueue = new ConcurrentQueue<Visitor>();
    }
}
