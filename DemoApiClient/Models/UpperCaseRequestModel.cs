using System.ComponentModel.DataAnnotations;

namespace DemoApiClient.Models
{
    /// <summary>
    /// Model for making a request to uppercase as string
    /// </summary>
    public class UpperCaseRequestModel
    {
        [Required]
        public string Input { get; set; }
    }
}
