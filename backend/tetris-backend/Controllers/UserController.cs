using tetris_backend.Models;
using tetris_backend.Services;
using Microsoft.AspNetCore.Mvc;
using tetris_backend.DTOModels;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Driver;

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

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<List<UserDTO>> Get()
        {
            var usersDB = await _userService.GetAsync();
            var shopItems = await _shopItemService.GetAsync();
            List<UserDTO> usersDTO = new List<UserDTO>();

            foreach (var userDB in usersDB)
            {
                UserDTO userDTO = new UserDTO();
                userDTO.Id = userDB.id;
                userDTO.Username = userDB.username;
                userDTO.Email = userDB.email;
                userDTO.Role = userDB.role;
                userDTO.Banned = userDB.banned;
                userDTO.JoinDate = userDB.joinDate;
                userDTO.LastOnlineDate = userDB.lastOnlineDate;
                userDTO.Coins = userDB.coins;
                userDTO.Scores = userDB.scores;
                userDTO.Friends = userDB.friends;

                if (userDB.shopItems != null)
                {
                    List<ShopItem> userDTOshopItems = new List<ShopItem>();
                    for (int i = 0; i < userDB.shopItems.Length; i++)
                    {
                        var shopItem = shopItems.Find(x => x.Id == userDB.shopItems[i]);
                        if (shopItem != null)
                        {
                            userDTOshopItems.Add(shopItem);
                        }
                    }
                    userDTO.ShopItems = userDTOshopItems.ToArray();
                }
                usersDTO.Add(userDTO);
            }
            return usersDTO;
        }

        [Authorize]
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<UserDTO>> Get(string id)
        {
            var userDB = await _userService.GetAsync(id);

            if (userDB is null)
            {
                return NotFound();
            }

            UserDTO userDTO = new UserDTO();
            userDTO.Id = userDB.id;
            userDTO.Username = userDB.username;
            userDTO.Email = userDB.email;
            userDTO.Role = userDB.role;
            userDTO.Banned = userDB.banned;
            userDTO.JoinDate = userDB.joinDate;
            userDTO.LastOnlineDate = userDB.lastOnlineDate;
            userDTO.Coins = userDB.coins;
            userDTO.Scores = userDB.scores;
            userDTO.Friends = userDB.friends;

            if (userDB.shopItems != null && userDB.shopItems.Length > 0)
            {
                var shopItems = await _shopItemService.GetAsync();
                List<ShopItem> userDTOshopItems = new List<ShopItem>();
                for (int i = 0; i < userDB.shopItems.Length; i++)
                {
                    var shopItem = shopItems.Find(x => x.Id == userDB.shopItems[i]);
                    if (shopItem != null)
                    {
                        userDTOshopItems.Add(shopItem);
                    }
                }
                userDTO.ShopItems = userDTOshopItems.ToArray();
            }

            return userDTO;
        }


        [HttpGet("{email}")]
        public async Task<ActionResult<UserDTO>> GetBasedOnEmail(string email)
        {
            var userDB = await _userService.GetBasedOnEmailAsync(email);

            if (userDB is null)
            {
                return null;
            }

            UserDTO userDTO = new UserDTO();
            userDTO.Id = userDB.id;
            userDTO.Username = userDB.username;
            userDTO.Email = userDB.email;
            userDTO.Role = userDB.role;
            userDTO.Banned = userDB.banned;
            userDTO.JoinDate = userDB.joinDate;
            userDTO.LastOnlineDate = userDB.lastOnlineDate;
            userDTO.Coins = userDB.coins;
            userDTO.Scores = userDB.scores;
            userDTO.Friends = userDB.friends;

            if (userDB.shopItems != null && userDB.shopItems.Length > 0)
            {
                var shopItems = await _shopItemService.GetAsync();
                List<ShopItem> userDTOshopItems = new List<ShopItem>();
                for (int i = 0; i < userDB.shopItems.Length; i++)
                {
                    var shopItem = shopItems.Find(x => x.Id == userDB.shopItems[i]);
                    if (shopItem != null)
                    {
                        userDTOshopItems.Add(shopItem);
                    }
                }
                userDTO.ShopItems = userDTOshopItems.ToArray();
            }

            return userDTO;
        }


        [HttpGet("is-registered/{email}")]
        public async Task<ActionResult<bool>> IsEmailRegistered(string email)
        {
            var userDB = await _userService.GetBasedOnEmailAsync(email);

            if (userDB is null)
            {
                return false;
            }

            return true;
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Post(UserDTO newUserDTO)
        {
            User userDB = new User();
            userDB.username = newUserDTO.Username;
            userDB.email = newUserDTO.Email;
            userDB.role = newUserDTO.Role;
            userDB.banned = newUserDTO.Banned;
            userDB.joinDate = newUserDTO.JoinDate;
            userDB.lastOnlineDate = newUserDTO.LastOnlineDate;
            userDB.coins = newUserDTO.Coins;
            userDB.scores = newUserDTO.Scores;
            userDB.friends = newUserDTO.Friends;

            if (newUserDTO.ShopItems != null && newUserDTO.ShopItems.Length > 0)
            {
                List<string> shopItemIds = new List<string>();
                foreach (var item in newUserDTO.ShopItems)
                {
                    shopItemIds.Add(item.Id);
                }

                userDB.shopItems = shopItemIds.ToArray();
            }

            await _userService.CreateAsync(userDB);

            return CreatedAtAction(nameof(Get), new { id = userDB.id }, userDB);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, UserDTO updatedUserDTO)
        {
            var userDB = await _userService.GetAsync(id);

            if (userDB is null)
            {
                return NotFound();
            }

            userDB.username = updatedUserDTO.Username;
            userDB.email = updatedUserDTO.Email;
            userDB.role = updatedUserDTO.Role;
            userDB.banned = updatedUserDTO.Banned;
            userDB.joinDate = updatedUserDTO.JoinDate;
            userDB.lastOnlineDate = updatedUserDTO.LastOnlineDate;
            userDB.coins = updatedUserDTO.Coins;
            userDB.scores = updatedUserDTO.Scores;
            userDB.friends = updatedUserDTO.Friends;

            if (updatedUserDTO.ShopItems != null)
            {
                if (updatedUserDTO.ShopItems.Length != userDB.shopItems.Length)
                {
                    List<string> shopItemIds = new List<string>();
                    foreach (var item in updatedUserDTO.ShopItems)
                    {
                        shopItemIds.Add(item.Id);
                    }

                    userDB.shopItems = shopItemIds.ToArray();
                }
            }

            await _userService.UpdateAsync(id, userDB);

            return NoContent();
        }


        [Authorize(Roles = "admin")]
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
