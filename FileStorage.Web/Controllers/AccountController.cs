using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FileStorage.BLL.Model;
using FileStorage.BLL.Service.Infrastructure;
using FileStorage.DAL.Model;
using FileStorage.Web.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FileStorage.Web.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(
            IConfiguration configuration,
            UserManager<User> userManager,
            SignInManager<User> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request model");

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return Ok(await GenerateJwtToken(appUser));
            }

            return BadRequest("Wrong credentials");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request model");

            var user = new User
            {
                Email = model.Email,
                UserName = model.Email,
                LastName = model.LastName,
                FirstName = model.FirstName
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                Directory.CreateDirectory(Path.Combine("Files", user.Id.ToString()));
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, false);
                return Ok(await GenerateJwtToken(user));
            }
            return BadRequest(result.Errors?.ElementAt(0));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<object> MakeAdmin([FromBody] ChangeRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request model");

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return NotFound("No user with such email");

            if (await _userManager.IsInRoleAsync(user, "Admin"))
                return Ok();

            var result = await _userManager.AddToRoleAsync(user, "Admin");
            if (result.Succeeded)
                return Ok();
            return BadRequest("Cannot change role");
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<object> MakeUser([FromBody] ChangeRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request model");

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return NotFound("No user with such email");

            if (!await _userManager.IsInRoleAsync(user, "Admin"))
                return Ok();

            var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
            if (result.Succeeded)
                return Ok();
            return BadRequest("Cannot change role");
        }

        private async Task<object> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            AddRolesToClaims(claims, roles);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var now = DateTime.Now;
            var hours = Convert.ToDouble(_configuration["JwtExpiresHours"]);
            var expires = now.AddHours(hours);

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            var tokenWrap = new TokenWrap
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                CreateDate = now,
                ExpireDate = expires,
                LifeHours = hours,
                IsAdmin = roles.Contains("Admin"),
                Email = user.UserName,
                UserId = user.Id.ToString()
            };

            return tokenWrap;
        }

        private void AddRolesToClaims(List<Claim> claims, IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role);
                claims.Add(roleClaim);
            }
        }
    }
}