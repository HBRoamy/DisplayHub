using AppDomain.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    public class AccountsRepo : IAccountsRepo
    {
        private readonly UserManager<AppUser> userManager;
        //private readonly SignInManager<AppUser> signInManager;
        //private readonly IConfiguration configuration;

        public AccountsRepo(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
            //SignInManager<AppUser> signInManager, IConfiguration configuration
            //this.signInManager = signInManager;
            //this.configuration = configuration;
        }
        public async Task<IdentityResult> RegisterAsync(AppUserDTO newUserDetails) // there are fromQuery, fromRoute, fromURI and fromForm too 
        {
            //fails automatically when the email already exists due to the configuration in startup class
            var user = new AppUser
            {
                UserName = newUserDetails.DisplayName.ToLower(),
                Email = newUserDetails.Email.ToLower(),
                DisplayName = newUserDetails.DisplayName,

            };

            return await userManager.CreateAsync(user, newUserDetails.Password);//password added here so that its hashing happens

        }


       
    }
}
