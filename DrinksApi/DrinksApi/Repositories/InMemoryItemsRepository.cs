using DrinksApi.Dtos;
using DrinksApi.Entities;

namespace DrinksApi.Repositories;

class InMemoryItemsRepository : IItemsRepository
{
    #region Private fields

    private readonly List<DrinkItem> _drinks;
    private int _discount;

    #endregion

    #region Initialization

    public InMemoryItemsRepository()
    {
        _drinks = new List<DrinkItem>
        {
            new()
            {
                Type = DrinkType.ItalianCoffee,
                Price = 1.10m,
                Quantity = 0,
            },
            new()
            {
                Type = DrinkType.AmericanCoffee,
                Price = 1.70m,
                Quantity = 0,
            },
            new()
            {
                Type = DrinkType.Tea,
                Price = 2.50m,
                Quantity = 0,
            },
            new()
            {
                Type = DrinkType.Chocolate,
                Price = 3.99m,
                Quantity = 0,
            }
        };
    }

    #endregion

    public IEnumerable<DrinkItem> GetItems()
    {
        return _drinks;
    }

    public DrinkItem AddItem(int type)
    {
        var index = GetIndexOfItemWithTypeOrFail(type);
        _drinks[index].Quantity++;

        return _drinks[index];
    }

    public DrinkItem EditItem(int type, EditDrinkItemDto itemDto)
    {
        var index = GetIndexOfItemWithTypeOrFail(type);
        _drinks[index].Quantity = itemDto.Quantity;

        return _drinks[index];
    }

    public void ApplyDiscount(ApplyDiscountDto applyDiscountDto)
    {
        _discount = applyDiscountDto.Discount;
    }

    #region Private fields

    private int GetIndexOfItemWithTypeOrFail(int type)
    {
        var index = _drinks.FindIndex(item => (int)item.Type == type);

        if (index == -1)
        {
            throw new ItemNotFoundException();
        }

        return index;
    }

    #endregion
}

