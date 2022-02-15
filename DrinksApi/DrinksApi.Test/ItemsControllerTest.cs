using System;
using System.Collections.Generic;
using System.Linq;
using DrinksApi.Controllers;
using DrinksApi.Dtos;
using DrinksApi.Entities;
using DrinksApi.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace DrinksApi.Test
{
    public class ItemsControllerTest
    {
        #region Private fields

        private ItemsController _itemsController = null!;
        private IItemsRepository _itemsRepository = null!;
        private Random _random = null!;

        #endregion

        #region SetUp

        [SetUp]
        public void Setup()
        {
            _itemsRepository = A.Fake<IItemsRepository>();
            _itemsController = new ItemsController(_itemsRepository, A.Fake<ILogger<ItemsController>>());
            _random = new Random();
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_ShouldReturnItemsFromRepository()
        {
            var someItems = new List<DrinkItem>
            {
                ADrinkItem(),
                ADrinkItem()
            };
            A.CallTo(() => _itemsRepository.GetItems()).Returns(someItems);
            var expectedItems = ToExpectedItems(someItems);

            var result = _itemsController.GetAll();

            result.Result.Should().BeOfType<OkObjectResult>();
            (result.Result as OkObjectResult).Value.Should().BeEquivalentTo(expectedItems);

        }

        #endregion

        #region Add

        [Test]
        public void IfItemTypeIsUnknown_Add_ShouldReturnNotFound()
        {
            A.CallTo(() => _itemsRepository.AddItem(A<int>._)).Throws<ItemNotFoundException>();

            var result = _itemsController.Add((int)ARandomDrinkType());

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public void IfItemTypeKnown_GetAll_ShouldAddItemToRepository()
        {
            var drinkType = (int)ARandomDrinkType();
            var aDrinkItem = ADrinkItem();
            var expectedItem = ToExpectedItem(aDrinkItem);
            A.CallTo(() => _itemsRepository.AddItem(drinkType)).Returns(aDrinkItem);
            
            var result = _itemsController.Add(drinkType);

            A.CallTo(() => _itemsRepository.AddItem(drinkType)).MustHaveHappenedOnceExactly();
            result.Result.Should().BeOfType<CreatedResult>();
            ((result.Result as CreatedResult).Value as DrinkItemDto).Should().BeEquivalentTo(expectedItem);
        }

        #endregion

        #region Utility methods

        private DrinkType ARandomDrinkType()
        {
            return (DrinkType)_random.Next(0, 4);
        }

        private DrinkItem ADrinkItem()
        {
            return new DrinkItem
            {
                Quantity = _random.Next(),
                Price = _random.Next(),
                Type = ARandomDrinkType()
            };
        }

        private static IEnumerable<DrinkItemDto> ToExpectedItems(IEnumerable<DrinkItem> someItems)
        {
            return someItems.Select(ToExpectedItem);
        }

        private static DrinkItemDto ToExpectedItem(DrinkItem item)
        {
            return new DrinkItemDto((int)item.Type, item.Price, item.Quantity);
        }

        #endregion
    }
}