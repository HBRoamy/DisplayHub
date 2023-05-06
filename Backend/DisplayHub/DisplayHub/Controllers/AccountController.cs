using AppDomain.Entities;
using BusinessLayer.Interfaces;
using DisplayHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DisplayHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IAccountsRepo accountRepo;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAccountsRepo accountRepo, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.accountRepo = accountRepo;
            this.configuration = configuration;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel newUserDetails) // there are fromQuery, fromRoute, fromURI and fromForm too 
        {
            if (ModelState.IsValid)
            {
                var newUser = new AppUserDTO()
                {
                    Email = newUserDetails.Email.ToLower(),
                    DisplayName = newUserDetails.DisplayName.ToLower(),
                    Password = newUserDetails.Password
                };
                var result = await accountRepo.RegisterAsync(newUser);//password added here so that its hashing happens
                if (result.Succeeded)
                {
                    return Ok(result.Succeeded);
                }

            }
            return BadRequest("Either details are invalid or the email already exists.");
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            if (ModelState.IsValid)
            {

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, true, false); //model.email is matching it to username (PasswordsigninAsync requires username and not email so I copied username to email and added a new Full Name property)

                if (!result.Succeeded || result == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                    return Unauthorized();
                }
                var userBody = await userManager.FindByEmailAsync(model.Email.ToLower());
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Email),
                    new Claim("email",model.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var authSignInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256Signature)
                    );
                var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);
                //var currentUser = await userManager.GetUserAsync(User);
                return Ok(new { token = generatedToken, email = userBody.Email, displayName = userBody.DisplayName });
            }
            return BadRequest();
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserPublicInfo([FromQuery]string username)
        {
            if(!string.IsNullOrEmpty(username) && !string.IsNullOrWhiteSpace(username))
            {
                var user =await userManager.FindByNameAsync(username.ToLower());
                if(user!=null)
                {
                    return Ok(new { displayName = user.DisplayName, name = user.UserName });
                }
                return NotFound("User not present");
            }
            return BadRequest("Username invalid.");
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok(true);
        }
    }
}
