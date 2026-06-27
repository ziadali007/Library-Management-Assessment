using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using Shared.Register_LoginDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthService(UserManager<AppUser> _userManager, RoleManager<AppRole> _roleManager) : IAuthService
    {
        public async Task<(bool Success, IEnumerable<string> Errors)> RegisterMemberAsync(RegisterUserDto dto)
        {
            var user = new AppUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                RegistrationDate = DateTime.UtcNow,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return (false, result.Errors.Select(e => e.Description));
            }

            var roleResult = await _userManager.AddToRoleAsync(user, UserRole.Member);
            if (!roleResult.Succeeded)
            {
                return (false, roleResult.Errors.Select(e => e.Description));
            }

            return (true, Enumerable.Empty<string>());
        }
    }
}
