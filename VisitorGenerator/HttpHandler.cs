using CarWash.Api.Washing.ResponseModel;
using CarWash.Core.Entities.RequestModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CarWash.VisitorGenerator
{
    /// <summary>
    /// Http Handler
    /// </summary>
    /// <seealso cref="CarWash.VisitorGenerator.IHttpHandler" />
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
        /// Sends the visitor to queue.
        /// </summary>
        /// <param name="visitorIdentifier">The visitor identifier.</param>
        /// <returns></returns>
        public async Task<string> sendVisitorToQueue(Guid visitorIdentifier)
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(this.configuration["WashingApiEndpoint"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri);

            var requestBody = new User()
            {
                Id = visitorIdentifier.ToString()
            };

            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            // List all Names.  
            HttpResponseMessage httpResponse = await client.SendAsync(requestMessage).ConfigureAwait(false);

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var response = JsonConvert.DeserializeObject<VisitorRequestResponse>(stringResponse);

            return response.Message;
        }
    }
}
