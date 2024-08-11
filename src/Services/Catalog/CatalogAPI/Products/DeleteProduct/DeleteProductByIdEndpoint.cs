
using CatalogAPI.Products.UpdateProduct;

namespace CatalogAPI.Products.DeleteProduct;

public record DeleteProductResponse(bool IsSuccess);


public class DeleteProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async(Guid Id, ISender sender) =>
        {
            var res = await sender.Send(new DeleteProductByIdCommand(Id));
            var response = res.Adapt<DeleteProductResponse>();

            return Results.Ok(response);

        }).WithName("Delete Product")
                .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Product")
                .WithDescription("Delete Product");
    }
}
