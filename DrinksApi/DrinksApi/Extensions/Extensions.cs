using DrinksApi.Dtos;
using DrinksApi.Entities;

namespace DrinksApi.Extensions
{
    public static class Extensions
    {
        public static DrinkItemDto AsDto(this DrinkItem drinkItem)
        {
            return new DrinkItemDto((int)drinkItem.Type, drinkItem.Price, drinkItem.Quantity);
        }
    }
}
