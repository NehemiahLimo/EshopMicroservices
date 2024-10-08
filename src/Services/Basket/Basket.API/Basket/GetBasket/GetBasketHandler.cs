﻿using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;
//using BuildingBlocks.CQRS;

namespace Basket.API.Basket.GetBasket;


public record   GetBasketQuery(string UserName)  : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

public class GetBasketHandler(IBasketRepository repo) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public  async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var data = await repo.GetBasket(query.UserName);
        return new GetBasketResult(data);
    }
}
