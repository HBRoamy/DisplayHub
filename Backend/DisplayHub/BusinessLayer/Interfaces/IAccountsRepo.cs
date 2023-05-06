using Microsoft.AspNetCore.Identity;
using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAccountsRepo
    {
        public Task<IdentityResult> RegisterAsync(AppUserDTO newUserDetails);
    }
}
