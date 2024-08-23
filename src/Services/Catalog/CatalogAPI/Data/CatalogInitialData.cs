using Marten.Schema;

namespace CatalogAPI.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
            return;

        //UPSERT
        session.Store<Product>(GetPreConfiguredProducts());
        await session.SaveChangesAsync();


    }

    private static IEnumerable<Product> GetPreConfiguredProducts() => new List<Product>()
    {
        new Product()
        {
            Id = Guid.NewGuid(),
            Name= "Mazda Docs",
            Description="Android Auto",
            Category= new List<string>{ "Updates" },
            ImageFile="/docs/newfolder/test",
            Price=15000
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Mazda Axela",
            Description = "Apple Carplay",
            Category = new List<string> { "Updates" },
            ImageFile = "/docs/newfolder/test",
            Price = 15000
        }
    };
}
