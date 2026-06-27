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

            var isUpdated = await serviceManager.BookService.UpdateBookAsync(dto);

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

        #region Category management

        [HttpPost("categories")]
        public async Task<IActionResult> CreateCategory([FromBody] AddCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await serviceManager.CategoryService.AddCategoryAsync(dto);
            return Ok("Category added successfully.");
        }

        [HttpPut("categories")]
        public async Task<IActionResult> UpdateCategory([FromBody] AddCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var isUpdated = await serviceManager.CategoryService.UpdateCategoryAsync(dto);
            if (!isUpdated)
                return NotFound($"Category record with ID {dto.Id} could not be found or updated.");
            return Ok("Category updated successfully.");
        }

        [HttpDelete("categories/{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var isDeleted = await serviceManager.CategoryService.DeleteCategoryAsync(id);
            if (!isDeleted)
                return NotFound($"Category record with ID {id} does not exist.");
            return Ok("Category deleted successfully.");
        }
        #endregion

        #region Author management

        [HttpGet("authors/{id:int}")]
        public async Task<ActionResult<AuthorResultDto>> GetAuthorById(int id)
        {
            var author = await serviceManager.AuthorService.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound($"Author record with ID {id} does not exist.");
            return Ok(author);
        }

        [HttpPost("authors")]
        public async Task<IActionResult> CreateAuthor([FromBody] AddAuthorDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result=await serviceManager.AuthorService.AddAuthorAsync(dto);
            return Ok(result);
        }

        [HttpPut("authors")]
        public async Task<IActionResult> UpdateAuthor([FromBody] AddAuthorDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var isUpdated = await serviceManager.AuthorService.UpdateAuthorAsync(dto);
            if (isUpdated is null)
                return NotFound($"Author record with ID {dto.Id} could not be found or updated.");
            return Ok(isUpdated);
        }

        [HttpDelete("authors/{id:int}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var isDeleted = await serviceManager.AuthorService.DeleteAuthorAsync(id);
            if (!isDeleted)
                return NotFound($"Author record with ID {id} does not exist.");
            return Ok("Author deleted successfully.");
        }


        #endregion

        #region Publisher management
        [HttpGet("users/{id:int}")]
        public async Task<ActionResult<AuthorResultDto>> GetUserById(int id)
        {
            var user = await serviceManager.PublisherService.GetPublisherByIdAsync(id);
            if (user == null)
                return NotFound($"User record with ID {id} does not exist.");
            return Ok(user);
        }

        [HttpPost("Publishers")]
        public async Task<IActionResult> CreatePublisher([FromBody] AddPublisherDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await serviceManager.PublisherService.AddPublisherAsync(dto);
            return Ok(result);
        }

        [HttpPut("Publishers")]
        public async Task<IActionResult> UpdatePublisher([FromBody] AddPublisherDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var isUpdated = await serviceManager.PublisherService.UpdatePublisherAsync(dto);
            if (isUpdated is null)
                return NotFound($"Publisher record with ID {dto.Id} could not be found or updated.");
            return Ok(isUpdated);
        }
        #endregion

        #region Language management
        [HttpGet("languages/{id:int}")]
        public async Task<ActionResult<LanguageResultDto>> GetLanguageById(int id)
        {
            var language = await serviceManager.LanguageService.GetLanguageByIdAsync(id);
            if (language == null)
                return NotFound($"Language record with ID {id} does not exist.");
            return Ok(language);
        }

        [HttpPost("languages")]
        public async Task<IActionResult> CreateLanguage([FromBody] AddLanguageDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var lang=await serviceManager.LanguageService.AddLanguageAsync(dto);
            return Ok(lang);
        }

        [HttpPut("languages")]
        public async Task<IActionResult> UpdateLanguage([FromBody] AddLanguageDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var isUpdated = await serviceManager.LanguageService.UpdateLanguageAsync(dto);
            if (isUpdated is null)
                return NotFound($"Language record with ID {dto.Id} could not be found or updated.");
            return Ok(isUpdated);
        }

        [HttpDelete("languages/{id:int}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            var isDeleted = await serviceManager.LanguageService.DeleteLanguageAsync(id);
            if (!isDeleted)
                return NotFound($"Language record with ID {id} does not exist.");
            return Ok("Language deleted successfully.");
        }
        #endregion

    }
}
