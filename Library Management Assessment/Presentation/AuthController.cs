using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Register_LoginDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (success, errors) = await serviceManager.AuthService.RegisterMemberAsync(dto);

            if (!success)
            {
                return BadRequest(new { message = "Registration failed", errors });
            }

            return Ok(new { message = "User registered successfully as a Member." });
        }

    }
}
