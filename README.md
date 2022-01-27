#  Requirements:


1. Use source control to manage the files in your implementation. 

2. Write a microservice that exposes a very simple REST API with one endpoint. That
endpoint accepts a string and returns the string reversed and shifted to all uppercase.
For instance:

    % curl -X POST -d '{"data": "hello, world!"}'
    http://localhost:5000/v1/
    {"data": "!DLROW ,OLLEH"}

    You can reverse the string however you want, but you must uppercase it by making an API call to
    http://shoutcloud.io/.

3. Write automation that will deploy your microservice to your favorite cloud platform.
Deploy the service as if it were a production deployment -- make it resilient to failure and
scalable for load. You can use whatever tools you want, but it must result in code that
you can send us. 

4. Write documentation explaining how your deployment process works. Also consider
what other services, processes, and so on would be required to create a full environment
suited to production use.

## Finished Demo

#### Live Azure API

**Endpoint**: https://petal-rules.azurewebsites.net/api/v1/

**Method**: Post

**JSON Request Body Example:** 
{ "data" : "selur latep"}

**Json Response Body Example:**
{"data": "PETAL RULES"}

## Things I did

1. Create an api endpoint that reverse the string from the post body.
2. Used ShoutCloud.io to uppercase string.
3. Return string in the body with propety "data".
4. Two tests:
   + One unit test for the extension method that is reversing the string.
   + One sudo integration test for the api call to uppercase the string.
5. Automated CI Pipeline for PRs that does the following:
   + Restores Nuget Packeges and Dependencies.
   + Builds the full solution (Not just the project).
   + Runs the tests.
6. Automated Continous Delivery Pipleline to deploy the function app to Azure when a PR is merged into Main.


## Thing TODO
1. Create smoke tests to run on against Azure endpoint after deployment.
2. Create integration tests to test against Azure Endpoint after deployment.
3. Add Full Test Pipeline After PR merge into Main to:
   + create a test resource in Azure using Terroform
   + run smoke and integratin tests
   + destroy test Azure resource
4. Add to trigger for current Continous Delivery Pipeline to only deploy if Full Test Pipeline is successful.
5. Powershell script for dev to create resource in Azure for development and testing.
