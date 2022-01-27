using System;
using System.Threading.Tasks;
using System.Web.Http;
using DemoApiClient.Models;
using DemoApiClient.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace DemoApiClient.Functions
{
    public class DemoFunctions
    {
        #region Private Fields

        private readonly ILogger<DemoFunctions> _Log;

        #endregion

        #region Constructors

        public DemoFunctions(ILogger<DemoFunctions> log)
        {
            _Log = log;
        }

        #endregion

        #region Private Methods

        private static async Task<T> SerializeToModel<T>(HttpRequest request)
        {
            string model = await request.ReadAsStringAsync();

            if (string.IsNullOrEmpty(model)) 
                throw new ArgumentException("The request body was null or empty");

            return JsonSerializer.Deserialize<T>(model);
        }

        #endregion

        #region Function Endpoints

        [Microsoft.Azure.WebJobs.FunctionName("DemoFunction_Transform_V")]
        public async Task<IActionResult> Transform([Microsoft.Azure.WebJobs.HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1")]
            HttpRequest request)
        {
            _Log.LogTrace("Request received for transform functions");

            try
            {

                TransformRequestModel model = await SerializeToModel<TransformRequestModel>(request);

                _Log.LogInformation($"Word to transform: {model.TransformString}");

                if (string.IsNullOrEmpty(model.TransformString))
                    throw new ArgumentException("Word to transform is null or empty.");
                
                string reservedString = TransformUtility.Reverse(model.TransformString);

                _Log.LogInformation($"Word reversed: {reservedString}");

                UpperCaseResponseModel result = await ExternalApiUtility.Uppercase<UpperCaseRequestModel, UpperCaseResponseModel>(new UpperCaseRequestModel
                {
                    Input = reservedString
                });

                _Log.LogInformation($"All good in the hood. Uppercase word to return: {result.Output}");

                return new OkObjectResult(new TransformResponseModel
                {
                    Data = result.Output
                });
            }
            //we could make a custom exception if something else is wrong with the request body, but you get the point.
            //I wil just use the Argument Exception to catch a specific exception and return a specific Http Code 400
            //In this function, this exception will only be thrown if the data value is null or empty
            catch (ArgumentException e)
            {
                _Log.LogError("Invalid Operation", e);
                return new BadRequestObjectResult("Bad request. Data is request body null, empty, or invalid");
            }
            catch (Exception e)
            {
                _Log.LogError("Internal Error Processing Request", e);
                //If there is any other problem we give back an internal server error 500
                //Depending on how much you want to expose, perhaps to another internal service,
                //you could make custom errors or an error model to return an enum. 
                return new InternalServerErrorResult();
            }
        }

        #endregion
    }
}
