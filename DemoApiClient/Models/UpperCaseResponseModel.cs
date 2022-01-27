using System.Text.Json.Serialization;

namespace DemoApiClient.Models
{
    ///Response from making an upper case api request
    public class UpperCaseResponseModel
    {
        [JsonPropertyName("INPUT")]
        public string Input { get; set; }

        [JsonPropertyName("OUTPUT")]
        public string Output { get; set; }
    }
}
