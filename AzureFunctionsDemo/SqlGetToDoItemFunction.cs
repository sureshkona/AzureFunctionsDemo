using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsDemo
{
    public class SqlGetToDoItemFunction
    {
        private readonly ILogger _logger;

        public SqlGetToDoItemFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SqlGetToDoItemFunction>();
        }

        [Function("SqlGetToDoItemFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
              [SqlInput(commandText: "select [Id], [order], [title], [url], [completed] from dbo.ToDo where Id = @Id",
                commandType: System.Data.CommandType.Text,
                parameters: "@Id={Query.id}",
                connectionStringSetting: "SqlDbConnection")] IEnumerable<ToDoItem> toDoItem)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

                return new ObjectResult(toDoItem.FirstOrDefault());
        }
    }
}
