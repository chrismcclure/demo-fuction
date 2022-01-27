using System;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;
using RestSharp;

namespace DemoApiClient.Utilities
{
    /// <summary>
    /// For the purpose of this project I am making a single api call
    /// If this was used by more that one method I would move this a service of some kind
    /// behind an interface so it's testable and could be called from dependency injection
    /// </summary>
    public static class ExternalApiUtility
    {
        /// <summary>
        /// For the purpose of this small demo I wanted to make a super simple generic utility to make single post api call
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<TOut> Uppercase<T,TOut>(T model)
        {
            //In a larger program I would create one HttpClient as a Singleton and use dependency injection
            //https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            //And I would make this base uri a configurable c# object like this
            //https://andrewlock.net/how-to-use-the-ioptions-pattern-for-configuration-in-asp-net-core-rc2/
            //to pull from local settings or azure configuration settings
            RestClient client = new RestClient("HTTP://API.SHOUTCLOUD.IO/V1/SHOUT")
            {
                Timeout = -1
            };

            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonSerializer.Serialize(model),  ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);

            if (response is null || string.IsNullOrEmpty(response.Content))
                throw new InvalidOperationException("The result from the api was null or empty string.");

            if (response.StatusCode != HttpStatusCode.OK)
                throw new InvalidOperationException($"The response from the api all was not {response.StatusCode}.  No beuno :(");

            return JsonSerializer.Deserialize<TOut>(response.Content);
        }
    }
}
