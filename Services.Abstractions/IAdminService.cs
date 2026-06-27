using Microsoft.AspNetCore.Identity;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAdminService
    {
        Task<IdentityResult> CreateUserWithRoleAsync(CreateUserDto model, string role);
    }
}
