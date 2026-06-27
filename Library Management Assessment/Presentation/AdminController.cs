using Domain.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/admin/books")]
    [Authorize(Roles = $"{UserRole.Admin},{UserRole.Librarian}")]
    public class AdminController : BaseController
    {
        private readonly IServiceManager serviceManager;
        public AdminController(IServiceManager serviceManager) : base(serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        #region System User Management 
        [HttpPost("create-admin")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateUserDto model)
        {
            var result = await serviceManager.AdminService.CreateUserWithRoleAsync(model, UserRole.Admin);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok("Admin created successfully.");
        }

        [HttpPost("create-librarian")]
        [Authorize(Roles = $"{UserRole.Admin},{UserRole.Librarian}")]
        public async Task<IActionResult> CreateLibrarian([FromBody] CreateUserDto model)
        {
            var result = await serviceManager.AdminService.CreateUserWithRoleAsync(model, UserRole.Librarian);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok("Librarian created successfully.");
        }

        [HttpPost("create-staff")]
        [Authorize(Roles = $"{UserRole.Admin},{UserRole.Librarian}")]
        public async Task<IActionResult> CreateStaff([FromBody] CreateUserDto model)
        {
            var result = await serviceManager.AdminService.CreateUserWithRoleAsync(model, UserRole.Staff);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok("Staff created successfully.");
        }
        #endregion

        #region Book management
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookResultDto>> GetBookById(int id)
        {
            try
            {
                var book = await serviceManager.BookService.GetBookByIdAsync(id);
                return Ok(book);
            }
            catch (KeyNotFoundException ex)
            {
                // Catches the service exception and returns a proper RESTful 404 response
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] AddBookDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await serviceManager.BookService.AddBookAsync(dto);

            return Ok("Book added successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isUpdated = await serviceManager.BookService.UpdateBookAsync(dto.Id, dto);

            if (!isUpdated)
                return NotFound($"Book record with ID {dto.Id} could not be found or updated.");

            return Ok("Book updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var isDeleted = await serviceManager.BookService.DeleteBookAsync(id);

            if (!isDeleted)
                return NotFound($"Book record with ID {id} does not exist.");

            return Ok("Book deleted successfully.");
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDto dto)
        {
            var success = await serviceManager.BookService.ReturnBookAsync(dto);
            if (!success)
                return BadRequest("Could not process return. Transaction might be invalid or already settled.");

            return Ok("Book marked returned successfully.");
        }
        #endregion



    }
}
