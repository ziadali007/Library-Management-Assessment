using Services.Abstractions;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> CreateUserWithRoleAsync(CreateUserDto model, string role)
        {
            var user = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return result;

            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<bool> DeleteUserByEmailAsync(DeleteUserByEmailDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return false; 
            }

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }
    }
}
