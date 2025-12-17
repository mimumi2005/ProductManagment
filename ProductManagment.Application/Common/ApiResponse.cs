namespace ProductManagment.Application.Common
{
    /// <summary>
    /// Unified success response format.
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Gets or sets the message associated with the service response.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the data associated with the service response.
        /// </summary>
        public dynamic? Data { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse"/> class.
        /// </summary>
        /// <param name="message">The message associated with the service response.</param>
        /// <param name="data">The data associated with the service response.</param>
        public ApiResponse(string message, dynamic? data = null)
        {
            Message = message;
            Data = data;
        }
    }
}
