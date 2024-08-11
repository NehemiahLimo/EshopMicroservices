using FluentValidation;
using Marten;

var builder = WebApplication.CreateBuilder(args);

//add  services to rhe container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMarten(opts =>
opts.Connection(builder.Configuration.GetConnectionString("Database")!)

).UseLightweightSessions();
var app = builder.Build();




//configure http requests
app.MapCarter();
app.MapGet("/", () => "Hello World!");

app.Run();
