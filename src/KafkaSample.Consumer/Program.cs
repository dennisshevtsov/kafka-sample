WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();
app.MapGet("/", () => "Kafka sample consumer working...");
app.Run();
