
using CatalogAPI.Products.GetProducts;

namespace CatalogAPI.Products.GetProductById;

public record GetProductByIdResponse(Product Product);


public class GetProductByIdEnpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapGet("/products/{id}", async(Guid Id, ISender sender) =>
       {
           var result = await sender.Send(new GetProductByIdQuery(Id)); 
           var response =result.Adapt<GetProductByIdResponse>();

           return Results.Ok(response);
       })
            .WithName("GetProductById")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product by ID")
                .WithDescription("Get Product  by ID");
    }
}
