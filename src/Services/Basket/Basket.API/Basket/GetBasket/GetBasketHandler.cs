using Basket.API.Models;
using BuildingBlocks.CQRS;
//using BuildingBlocks.CQRS;

namespace Basket.API.Basket.GetBasket;


public record   GetBasketQuery(string UserName)  : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Caart);

public class GetBasketHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public  async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        return new GetBasketResult(new ShoppingCart("swn"));
    }
}
