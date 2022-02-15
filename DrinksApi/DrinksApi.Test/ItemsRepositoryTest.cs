using System;
using DrinksApi.Dtos;
using DrinksApi.Entities;
using DrinksApi.Repositories;
using FluentAssertions;
using NUnit.Framework;

namespace DrinksApi.Test;

public class ItemsRepositoryTest
{
    #region Private fields

    private InMemoryItemsRepository _repository = null!;

    #endregion

    #region SetUp

    [SetUp]
    public void Setup()
    {
        _repository = new InMemoryItemsRepository();
    }

    #endregion

    #region Pay

    [Test]
    public void IfTotalIsLessThan10_Pay_ShouldSucceedWithBothPaymentMethods([Values]PaymentMethod paymentMethod)
    {
        _repository.AddItem(1);

        _repository.GetTotal().Should().BeGreaterThan(0);
        _repository.Invoking(r => r.Pay(paymentMethod)).Should().NotThrow();
        _repository.GetTotal().Should().Be(0);
    }

    [Test]
    public void IfTotalIsMoreThan10_Pay_ShouldSucceedWithCard()
    {
        _repository.EditItem(1, new EditDrinkItemDto(10));

        _repository.GetTotal().Should().BeGreaterThan(10);
        _repository.Invoking(r => r.Pay(PaymentMethod.CreditCard)).Should().NotThrow();
        _repository.GetTotal().Should().Be(0);
    }

    [Test]
    public void IfTotalIsMoreThan10_Pay_ShouldFailWithCash()
    {
        _repository.EditItem(1, new EditDrinkItemDto(10));
        var oldTotal = _repository.GetTotal();
        
        oldTotal.Should().BeGreaterThan(10);
        _repository.Invoking(r => r.Pay(PaymentMethod.Cash))
            .Should()
            .Throw<InvalidPaymentMethodException>();
        _repository.GetTotal().Should().Be(oldTotal);
    }

    #endregion
}