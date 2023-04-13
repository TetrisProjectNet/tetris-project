using tetris_backend.Models;
using tetris_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace tetris_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ShopItemService _shopItemService;

        public UserController(UserService userService, ShopItemService shopItemService)
        {
            _userService = userService;
            _shopItemService = shopItemService;
        }


        [HttpGet]
        public async Task<List<User>> Get()
        {
            var users = await _userService.GetAsync();

            foreach (var user in users)
            {
                if (user.ShopItems.Length > 0)
                {
                    for (int i = 0; i < user.ShopItems.Length; i++)
                    {
                        ShopItem shopItem = await _shopItemService.GetAsync(user.ShopItems[i]);
                        user.ShopItems[i] = shopItem;
                    }
                    user.ShopItems = user.ShopItems.Where(c => c != null).ToArray();
                }
            }

            return users;
        }


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            if (user.ShopItems.Length > 0)
            {
                for (int i = 0; i < user.ShopItems.Length; i++)
                {
                    ShopItem shopItem = await _shopItemService.GetAsync(user.ShopItems[i]);
                    user.ShopItems[i] = shopItem;
                }
                user.ShopItems = user.ShopItems.Where(c => c != null).ToArray();
            }

            return user;
        }


        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetBasedOnEmail(string email)
        {
            var user = await _userService.GetBasedOnEmailAsync(email);

            if (user is null)
            {
                return null;
            }

            if (user.ShopItems.Length > 0)
            {
                for (int i = 0; i < user.ShopItems.Length; i++)
                {
                    ShopItem shopItem = await _shopItemService.GetAsync(user.ShopItems[i]);
                    user.ShopItems[i] = shopItem;
                }
                user.ShopItems = user.ShopItems.Where(c => c != null).ToArray();
            }

            return user;
        }



        [HttpPost]
        public async Task<IActionResult> Post(User newUser)
        {
            await _userService.CreateAsync(newUser);

            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }


        [HttpPatch("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, User updatedUser)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            updatedUser.Id = user.Id;

            await _userService.UpdateAsync(id, updatedUser);

            return NoContent();
        }


        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            await _userService.RemoveAsync(id);

            return NoContent();
        }

    }
}
