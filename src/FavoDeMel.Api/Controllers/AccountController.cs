using FavoDeMel.Application.DTO;
using FavorDeMel.Api.Configurations;
using FavorDeMel.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FavoDeMel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppSettings _appSettings;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            FavoDeMelDbContext context,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegistrationDTO userRegistration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false
                });
            }

            var user = new IdentityUser
            {
                UserName = userRegistration.Email,
                Email = userRegistration.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userRegistration.Password);
            
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    success = false
                });
            }

            await _signInManager.SignInAsync(user, false);
            await _userManager.AddToRoleAsync(user, userRegistration.Role);
            var token = await GenerateJwt(userRegistration.Email);

            return Ok(new
            {
                success = true,
                user = userRegistration,
                token = token
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDTO userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false
                });
            }

            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);
            
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(userLogin.Email);
                var token = await GenerateJwt(userLogin.Email);

                return Ok(new
                {
                    success = true,
                    user = user,
                    token = token
                });
            }

            return BadRequest(new
            {
                success = false
            });
        }

        // GET: api/Roles
        [HttpGet]
        [Route("roles")]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetRoles()
        {
            var list = await _roleManager.Roles.ToListAsync();
            return list;
        }

        private async Task<string> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidAt,
                Expires = DateTime.UtcNow.AddHours(_appSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
