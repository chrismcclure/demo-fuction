using System.Text.Json.Serialization;

namespace DemoApiClient.Models
{
    /// <summary>
    /// Request model for transforming string
    /// </summary>
    public class TransformRequestModel
    {
        /// <summary>
        /// Use the Json property name data and convert to C# property
        /// </summary>
        [JsonPropertyName("data")]
        public string TransformString { get; set; }
    }
}
