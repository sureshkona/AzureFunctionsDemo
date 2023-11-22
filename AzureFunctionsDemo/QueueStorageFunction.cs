using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsDemo
{
    public class QueueStorageFunction
    {
        private readonly ILogger<QueueStorageFunction> _logger;

        public QueueStorageFunction(ILogger<QueueStorageFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(QueueStorageFunction))]
        [QueueOutput("myqueue-items-destination", Connection = "QueueStorageConnection")]
        public string Run([QueueTrigger("myqueue-items", Connection = "QueueStorageConnection")] QueueMessage message)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");

            return message.MessageText;
        }
    }
}
