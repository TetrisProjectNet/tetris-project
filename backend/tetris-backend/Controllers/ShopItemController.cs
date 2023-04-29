using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using tetris_backend.Models;
using tetris_backend.Services;

namespace tetris_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShopItemController : ControllerBase
    {
        private readonly ShopItemService _shopItemService;

        public ShopItemController(ShopItemService shopItemService) =>
            _shopItemService = shopItemService;


        [HttpGet]
        public async Task<List<ShopItem>> Get() =>
            await _shopItemService.GetAsync();


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<ShopItem>> Get(string id)
        {
            var shopItem = await _shopItemService.GetAsync(id);

            if (shopItem is null)
            {
                return NotFound();
            }

            return shopItem;
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Post(ShopItem newShopItem)
        {
            await _shopItemService.CreateAsync(newShopItem);

            return CreatedAtAction(nameof(Get), new { id = newShopItem.Id }, newShopItem);
        }


        [Authorize(Roles = "admin")]
        [HttpPatch("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, ShopItem updatedShopItem)
        {
            var shopItem = await _shopItemService.GetAsync(id);

            if (shopItem is null)
            {
                return NotFound();
            }

            updatedShopItem.Id = shopItem.Id;

            await _shopItemService.UpdateAsync(id, updatedShopItem);

            return NoContent();
        }


        [Authorize(Roles = "admin")]
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var shopItem = await _shopItemService.GetAsync(id);

            if (shopItem is null)
            {
                return NotFound();
            }

            await _shopItemService.RemoveAsync(id);

            return NoContent();
        }

    }
}
