using DrinksApi.Dtos;
using DrinksApi.Entities;

namespace DrinksApi.Repositories
{
    public interface IItemsRepository
    {
        IEnumerable<DrinkItem> GetItems();
        DrinkItem AddItem(int type);
        DrinkItem EditItem(int type, EditDrinkItemDto itemDto);
        void ApplyDiscount(ApplyDiscountDto applyDiscountDto);
        decimal GetTotal();
        void Pay(PaymentMethod toPaymentMethod);
    }
}
