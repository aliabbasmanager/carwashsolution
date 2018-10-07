using System;
using System.Threading.Tasks;

namespace CarWash.VisitorGenerator
{
    /// <summary>
    /// HttpHandler interface
    /// </summary>
    public interface IHttpHandler
    {
        /// <summary>
        /// Sends the visitor to queue.
        /// </summary>
        /// <param name="visitorIdentifier">The visitor identifier.</param>
        /// <returns></returns>
        Task<string> sendVisitorToQueue(Guid visitorIdentifier);
    }
}