﻿
using CatalogAPI.Products.UpdateProduct;
using FluentValidation;

namespace CatalogAPI.Products.DeleteProduct;

public record DeleteProductByIdCommand(Guid Id): ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductByIdCommand>
{

    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product  ID is required");      

    }

}
internal class DeleteProductByIdHandler(IDocumentSession session, ILogger<DeleteProductByIdHandler> logger)
    : ICommandHandler<DeleteProductByIdCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductHandler.Handle called with {@Command}", command);

        /*var product = session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }
        session.Delete(product);*/

        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);

    }
}
