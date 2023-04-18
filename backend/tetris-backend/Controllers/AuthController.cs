using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using tetris_backend.DTOModels;
using tetris_backend.Services;

namespace tetris_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //public static User userDB = new User();
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(UserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            User userDB = new User();
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            userDB.username = request.Username;
            userDB.passwordHash = passwordHash;
            userDB.passwordSalt = passwordSalt;
            userDB.email = request.Email;
            userDB.role = request.Role;
            userDB.banned = request.Banned;
            userDB.joinDate = request.JoinDate;
            userDB.lastOnlineDate = request.LastOnlineDate;
            userDB.coins = request.Coins;
            userDB.scores = request.Scores;
            userDB.friends = request.Friends;

            if (request?.ShopItems?.Length > 0)
            {
                List<string> shopItemIds = new List<string>();
                foreach (var item in request.ShopItems)
                {
                    shopItemIds.Add(item?.Id);
                }

                userDB.shopItems = shopItemIds.ToArray();
            }

            await _userService.CreateAsync(userDB);

            return Ok(userDB);

            //return CreatedAtAction(nameof(UserController.Get), new { id = userDB.id }, userDB);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(string username, string password)
        {
            User userDB = await _userService.GetBasedOnUsernameAsync(username);

            if (userDB == null)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(password, userDB.passwordHash, userDB.passwordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(userDB);

            return Ok(token);
        }

        private string CreateToken(User userDB)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userDB.username),
                new Claim(ClaimTypes.Role, userDB.role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
