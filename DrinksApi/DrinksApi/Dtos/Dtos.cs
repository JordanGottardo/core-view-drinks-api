using System.ComponentModel.DataAnnotations;

namespace DrinksApi.Dtos
{
    public record DrinkItemDto(
        int Type, 
        decimal Price, 
        int Quantity);

    public record EditDrinkItemDto(
         [Required, Range(0, int.MaxValue)] int Quantity);
}
