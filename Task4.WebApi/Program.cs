using Task4.Persistence;
using Task4.WebApi;

var builder = WebApplication.CreateBuilder(args);
Startup.Init(builder)
    .ConfigureServices();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<RegisteredUserDbContext>();
        DbInitializer.Initialize(context);
        Startup.ConfigureApplicationPipeline(app);
    } catch (InvalidOperationException exception)
    {
        Console.WriteLine(exception.Message);
    }
}
app.Run();
