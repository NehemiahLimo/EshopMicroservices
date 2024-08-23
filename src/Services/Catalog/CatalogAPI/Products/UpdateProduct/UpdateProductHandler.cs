
using CatalogAPI.Products.CreateProduct;
using FluentValidation;

namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description,
        string ImageFile, decimal Price): ICommand<UpdateProductCommandResponse>;
public record UpdateProductCommandResponse(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{

    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product  ID is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").Length
            (2, 159).WithMessage("Name must be between 2 and 160 characters");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Not a valida Price tag");

    }

}

internal class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductCommandResponse>
{
    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductHandler.Handle called with {@Command}", command);

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }  
        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;  
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Update(product);
        await  session.SaveChangesAsync(cancellationToken);


        return new UpdateProductCommandResponse(true);
    
    }
}
