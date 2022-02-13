namespace DrinksApi.Entities;

public class DrinkItem
{
    public DrinkType Type { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }


}

public enum DrinkType
{
    ItalianCoffee = 0,
    AmericanCoffee = 1,
    Tea = 2,
    Chocolate = 3
}