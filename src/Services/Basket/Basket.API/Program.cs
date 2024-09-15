

using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Caching.Distributed;

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
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("Redis");

});
/*builder.Services.AddScoped<IBaseRequest>(provider =>
{
    var basketRespository = provider.GetRequiredService<BasketRepository>();
    return new CachedBasketRepository(basketRespository, provider.GetRequiredService<IDistributedCache>())
})
*/

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//health checks
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();


app.UseExceptionHandler(options => { });

//app.MapGet("/", () => "Hello World!");
app.MapCarter();

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
app.Run();
