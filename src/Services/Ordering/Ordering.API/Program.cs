var builder = WebApplication.CreateBuilder(args);

//services
//builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure HTTP request Pipeline


app.Run();
