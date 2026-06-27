using Shared;
using Shared.Register_LoginDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAuthService
    {
        Task<(bool Success, IEnumerable<string> Errors)> RegisterMemberAsync(RegisterUserDto dto);

        Task<string?> LoginAsync(LoginDto dto);
    }
}
