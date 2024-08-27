using Basket.API.Basket.GetBasket;
using Carter;
using Mapster;
using MediatR;

namespace Basket.API.Basket.DeleteBasket;
public record DeleteBasketRequest(string UserName);
public record DeleteBasketResponse(bool IsSuccess);


public class DeleteBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/delete/{UserName}", async (string UserName, ISender sender) =>
        {
            var result = sender.Send(new DeleteBasketCommand(UserName));
            var response = result.Adapt<DeleteBasketResponse>();
            return Results.Ok(response);

        })
        .WithName("Delete Shopping cart")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Basket data")
        .WithDescription("Delete Basket"); ;
        }
}
