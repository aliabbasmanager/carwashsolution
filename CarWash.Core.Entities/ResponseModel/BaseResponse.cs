namespace CarWash.Api.Washing.ResponseModel
{
    /// <summary>
    /// Base response class
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public string statusCode { get; set; }
    }
}
