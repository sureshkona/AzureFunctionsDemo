using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctionsDemo
{
    public class SqlUpsertDemoFunction
    {
        private readonly ILogger _logger;

        public SqlUpsertDemoFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SqlUpsertDemoFunction>();
        }

        // Visit https://aka.ms/sqltrigger to learn how to use this trigger binding
        [Function("SqlUpsertDemoFunction")]
        public void Run(
            [SqlTrigger("[dbo].[ToDo]", "SqlDbConnection")] IReadOnlyList<SqlChange<ToDoItem>> changes,
                FunctionContext context)
        {
            _logger.LogInformation("SQL Changes: " + JsonConvert.SerializeObject(changes));

            // var logger = context.GetLogger("ToDoTrigger");
            foreach (SqlChange<ToDoItem> change in changes)
            {
                ToDoItem toDoItem = change.Item;
                _logger.LogInformation($"Change operation: {change.Operation}");
                _logger.LogInformation($"Id: {toDoItem.Id}, Title: {toDoItem.title}, Url: {toDoItem.url}, Completed: {toDoItem.completed}");
            }

        }
    }

    public class ToDoItem
    {
        public Guid Id { get; set; }
        public int? order { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public bool? completed { get; set; }
    }

}
