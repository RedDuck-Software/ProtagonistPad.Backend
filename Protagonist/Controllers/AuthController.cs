using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Protagonist;
using Protagonist.Services.UserService;

namespace JwtWebApiTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static readonly User _user = new ();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetMe()
        {
            var userName = _userService.GetMyName();
            return Ok(userName);
        }

        [HttpPost("register")]
        public Task<ActionResult<User>> Register(UserDto request)
        {
            CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);
            _user.Username = request.Username;
            _user.PasswordHash = passwordHash;
            _user.PasswordSalt = passwordSalt;
            return Task.FromResult<ActionResult<User>>(Ok(_user));
        }

        [HttpPost("login")]
        public Task<ActionResult<string>> Login(UserDto request)
        {
            if (_user.Username != request.Username)
            {
                return Task.FromResult<ActionResult<string>>(BadRequest("_user not found."));
            }

            if (!VerifyPasswordHash(request.Password, _user.PasswordHash, _user.PasswordSalt))
            {
                return Task.FromResult<ActionResult<string>>(BadRequest("Wrong password."));
            }

            var token = CreateToken(_user);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);

            return Task.FromResult<ActionResult<string>>(Ok(token));
        }

        [HttpPost("refresh-token")]
        public Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!_user.RefreshToken.Equals(refreshToken, StringComparison.Ordinal))
            {
                return Task.FromResult<ActionResult<string>>(Unauthorized("Invalid Refresh Token."));
            }
            if(_user.TokenExpires < DateTime.Now)
            {
                return Task.FromResult<ActionResult<string>>(Unauthorized("Token expired."));
            }

            var token = CreateToken(_user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);

            return Task.FromResult<ActionResult<string>>(Ok(token));
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

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            _user.RefreshToken = newRefreshToken.Token;
            _user.TokenCreated = newRefreshToken.Created;
            _user.TokenExpires = newRefreshToken.Expires;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var credits = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credits);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
