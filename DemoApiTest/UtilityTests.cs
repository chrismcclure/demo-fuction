using System;
using System.Threading.Tasks;
using DemoApiClient.Models;
using DemoApiClient.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace DemoApiTest
{
    [TestClass]
    public class UtilityTests
    {
        #region Fields

        private const string _DemoTest = "demotest";

        #endregion

        #region Private Methods

        private static void PrintResults(string results)
        {
            Console.WriteLine($"Results {results}");
        }

        #endregion

        #region Test Methods

        [TestMethod]
        public void TransformUtilityTest()
        {
            string reverse = "tsetomed";
            var result = _DemoTest.Reverse();
            PrintResults(result);
            result.ShouldBe(reverse, "The word wasn't reversed properly.");
        }

        /// <summary>
        /// A couple things about this one
        /// 1. If this were a unit test in the real world I would mock up a response, NOT call to a real endpoint for a unit test.
        /// 2. This is more of an integration test.  As part of TDD I was able to quickly ensure my utility worked, so I'll let it in here.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ExternalApiUtilityTest()
        {
            UpperCaseRequestModel requestModel = new UpperCaseRequestModel
            {
                Input = _DemoTest
            };
            var results = await ExternalApiUtility.Uppercase<UpperCaseRequestModel, UpperCaseResponseModel>(requestModel);
            PrintResults(results.Output);
            _DemoTest.ToUpper().ShouldBe(results.Output);
        }

        #endregion
    }
}
