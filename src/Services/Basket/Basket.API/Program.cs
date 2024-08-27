using BuildingBlocks.Behaviours;
using Carter;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
//services
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssembly(assembly);
    c.AddOpenBehavior(typeof(ValidationBehavior<,>));
    c.AddOpenBehavior(typeof(LoggingBehaviour<,>));


});


//app.MapGet("/", () => "Hello World!");
app.MapCarter();
app.Run();
