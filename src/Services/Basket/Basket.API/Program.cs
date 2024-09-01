

using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

//services
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(c =>
{
    c.RegisterServicesFromAssembly(assembly);
    c.AddOpenBehavior(typeof(ValidationBehavior<,>));
    c.AddOpenBehavior(typeof(LoggingBehaviour<,>));


});
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x=>x.UserName);
   // opts.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
}).UseLightweightSessions();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();


app.UseExceptionHandler(options => { });

//app.MapGet("/", () => "Hello World!");
app.MapCarter();
app.Run();
