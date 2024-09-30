using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//services
builder.Services.AddApplicationServices()
    .AddInfrastructure(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

// Configure HTTP request Pipeline


app.Run();
