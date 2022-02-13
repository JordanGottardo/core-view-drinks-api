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

        public static PaymentMethod ToPaymentMethod(this PayTotalDto payTotalDto)
        {
            return payTotalDto.Method switch
            {
                0 => PaymentMethod.Cash,
                1 => PaymentMethod.CreditCard,
                _ => throw new ArgumentOutOfRangeException(nameof(payTotalDto.Method), payTotalDto.Method.ToString())
            };
        }
    }
}
