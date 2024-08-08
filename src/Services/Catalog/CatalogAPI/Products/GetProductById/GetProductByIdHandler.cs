

namespace CatalogAPI.Products.GetProductById;

public record GetProductByIdQuery(Guid Id): IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);


internal class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdHandler> logger) :
IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);
        var productinfo = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (productinfo is null)
        {
            throw new ProductNotFoundException();
        }

        return new GetProductByIdResult(productinfo);


    }
}

