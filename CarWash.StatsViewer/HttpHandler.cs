using CarWash.Core.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CarWash.StatsViewer
{
    /// <summary>
    /// Http Handler
    /// </summary>
    /// <seealso cref="CarWash.StatsViewer.IHttpHandler" />
    public class HttpHandler : IHttpHandler
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpHandler"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public HttpHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        /// <summary>
        /// Gets the washing statistics.
        /// </summary>
        /// <returns>The statistics</returns>
        public async Task<OperationalStatistics> GetWashingStatistics()
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(this.configuration["WashingApiStatisticsEndpoint"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            HttpResponseMessage httpResponse = await client.SendAsync(requestMessage).ConfigureAwait(false);

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var response = JsonConvert.DeserializeObject<OperationalStatistics>(stringResponse);

            return response;
        }
    }
}
