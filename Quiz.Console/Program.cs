using Cocona;
using Microsoft.Extensions.Logging;
using Quiz.Console;

var builder = CoconaApp.CreateBuilder();
builder.Services.ConfigureConsoleServices(builder.Configuration);

var app = builder.Build();
app.AddCommand("test",(MyService myService)=>
{
     myService.Hello("this is message");
});
app.Run();

class MyService
{
    private readonly ILogger _logger;

    public MyService(ILogger<MyService> logger)
    {
        _logger = logger;
    }

    public void Hello(string message)
    {
        _logger.LogInformation(message);
        System.Console.WriteLine(message);
    }
}

