using AuthorsWebAPI.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace AuthorsWebAPI.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }
        [HttpPost("register")] //   api/account/register
        public async Task<ActionResult<AuthenticationResponse>> Register(UserCredentials userCredentials) 
        {
            var user = new IdentityUser {
                UserName= userCredentials.Email, 
                Email = userCredentials.Email 
            };
            var result = await userManager.CreateAsync(user, userCredentials.Password);

            if (result.Succeeded)
            {
                return await GenerateToken(userCredentials);
            }
            else 
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(UserCredentials userCredentials) 
        {
            var result = await this.signInManager.PasswordSignInAsync(
                    userCredentials.Email,
                    userCredentials.Password,
                    isPersistent: false,
                    lockoutOnFailure: false
                );

            if (result.Succeeded)
            {
                return await GenerateToken(userCredentials);
            }
            else 
            {
                return BadRequest("Wrong login");
            }
        }
        [HttpGet("RefreshToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        private async Task<ActionResult<AuthenticationResponse>> Refresh() 
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var userCredentials = new UserCredentials
            {
                Email = email,
            };

            return await GenerateToken(userCredentials);
        }
        private async Task<AuthenticationResponse> GenerateToken(UserCredentials userCredentials) 
        {
            var claims = new List<Claim>()
            {
                new Claim("email", userCredentials.Email)
            };

            var user = await userManager.FindByEmailAsync(userCredentials.Email);
            var claimsDb = await userManager.GetClaimsAsync(user);

            claims.AddRange(claimsDb);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);
            return new AuthenticationResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration
            };
        }

        [HttpPost("MakeAdmin")]
        public async Task<ActionResult> MakeAdmin(AdminEditDTO adminEditDTO)
        {
            var user = await userManager.FindByEmailAsync(adminEditDTO.Email);
            await userManager.AddClaimAsync(user, new Claim("isAdmin", "1"));
            return NoContent();
        }
        [HttpPost("RemoveAdmin")]
        public async Task<ActionResult> RemoveAdmin(AdminEditDTO adminEditDTO)
        {
            var user = await userManager.FindByEmailAsync(adminEditDTO.Email);
            await userManager.RemoveClaimAsync(user, new Claim("isAdmin", "1"));
            return NoContent();
        }
    }
}
