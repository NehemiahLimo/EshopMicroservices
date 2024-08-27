namespace Basket.API.Models;

public class ShoppingCartItem
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
    public decimal Price { get; set; } = 0;
    public string Color { get; set; }
    public string ProductName { get; set; }
}
