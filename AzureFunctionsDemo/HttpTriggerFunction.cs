using System.Net;
using Azure.Storage.Queues.Models;
using Azure.Storage.Queues;
using System.Text;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsDemo
{
    public class HttpTriggerFunction
    {
        private readonly ILogger _logger;

        public HttpTriggerFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpTriggerFunction>();
        }

        [Function("HttpTriggerFunction")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var name = req.Query["name"];
            var queueMessage = "Http Function Triggered From " + name;
          
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            QueueClient queueClient = new QueueClient("your queue storage connection", "myqueue-items-destination");
            queueClient.SendMessage(Convert.ToBase64String(Encoding.UTF8.GetBytes(queueMessage)));

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
