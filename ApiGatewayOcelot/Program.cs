using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);



builder.Configuration.AddJsonFile("GatewayConfig.json");
builder.Services.AddOcelot();


var app = builder.Build();
// Use Ocelot middleware
await app.UseOcelot();
app.MapGet("/", () => "Hello World!");

app.Run();
