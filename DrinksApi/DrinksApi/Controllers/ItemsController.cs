using System.ComponentModel.DataAnnotations;
using DrinksApi.Dtos;
using DrinksApi.Extensions;
using DrinksApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DrinksApi.Controllers
{
    [ApiController]
    [Route("basket")]
    public class ItemsController : ControllerBase
    {
        #region Private fields

        private readonly IItemsRepository _itemsRepository;
        private readonly ILogger<ItemsController> _logger;

        #endregion

        #region Initialization

        public ItemsController(IItemsRepository itemsRepository, ILogger<ItemsController> logger)
        {
            _itemsRepository = itemsRepository;
            _logger = logger;
        }

        #endregion

        [HttpGet("items")]
        public ActionResult<IEnumerable<DrinkItemDto>> GetAll()
        {
            var items = _itemsRepository.GetItems().ToList();

            _logger.LogInformation($"Retrieved {items.Count} items");

            return Ok(items.Select(item => item.AsDto()));
        }

        [HttpPost("items/{type}")]
        public ActionResult<IEnumerable<DrinkItemDto>> Add(int type)
        {
            try
            {
                var item = _itemsRepository.AddItem(type);

                return Created(nameof(GetAll), item);
            }
            catch (ItemNotFoundException e)
            {
                _logger.LogError(e, "item not found");
                return NotFound();
            }
        }

        [HttpPatch("items/{type}")]
        public ActionResult<IEnumerable<DrinkItemDto>> ModifyQuantity(int type, EditDrinkItemDto itemDto)
        {
            try
            {
                var item = _itemsRepository.EditItem(type, itemDto);

                return Created(nameof(GetAll), item);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "item not found");
                return NotFound();
            }
        }

        [HttpPost("discount")]
        public ActionResult Add(ApplyDiscountDto applyDiscountDto)
        {
            _itemsRepository.ApplyDiscount(applyDiscountDto);

            return NoContent();
        }

    }

}