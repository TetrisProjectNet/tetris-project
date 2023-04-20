using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using tetris_backend.DTOModels;
using tetris_backend.Models;
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
        private readonly VerificationService _verificationService;

        public AuthController(UserService userService, IConfiguration configuration, VerificationService verificationService)
        {
            _userService = userService;
            _configuration = configuration;
            _verificationService = verificationService;
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetLoggedUser()
        {
            var userName = _userService.GetLoggedUser();
            return Ok(userName);
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

            if (request?.ShopItems != null && request?.ShopItems?.Length > 0)
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


        [HttpPatch("reset-password/{email}/{newPassword}/{code}")]
        public async Task<ActionResult<User>> ResetPassword(string email, string newPassword, string code)
        {
            var userDB = await _userService.GetBasedOnEmailAsync(email);
            var verification = await _verificationService.GetBasedOnEmailAsync(email);

            if (userDB == null || verification == null)
            {
                return BadRequest("Email not registered.");
            }

            if (verification.Code != code)
            {
                return BadRequest("You are not verified.");
            }

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            await _userService.UpdatePasswordAsync(userDB.id, passwordHash, passwordSalt);
            await _verificationService.RemoveBasedOnEmailAsync(email);

            return Ok("Your password changed.");
        }


        [HttpPost("login/{username}/{password}")]
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

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, userDB);

            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken(string id)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var userDB = await _userService.GetAsync(id);

            if (!userDB.refreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (userDB.tokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = CreateToken(userDB);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken, userDB);

            return Ok(token);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private async void SetRefreshToken(RefreshToken newRefreshToken, User userDB)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            userDB.refreshToken = newRefreshToken.Token;
            userDB.tokenCreated = newRefreshToken.Created;
            userDB.tokenExpires = newRefreshToken.Expires;

            await _userService.UpdateAsync(userDB.id, userDB);
        }

        private string CreateToken(User userDB)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("id", userDB.id),
                new Claim(ClaimTypes.Name, userDB.username),
                new Claim(ClaimTypes.Role, userDB.role),

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
