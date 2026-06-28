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
    [Route("api/[controller]")]
    [Authorize]
    public class BaseController(IServiceManager serviceManager) : ControllerBase
    {
        #region Books Shared Endpoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResultDto>>> GetAllBooks()
        {
            var books = await serviceManager.BookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BookResultDto>>> SearchBooks([FromQuery] BookSearchFilter filter)
        {
            var books = await serviceManager.BookService.SearchBooksAsync(filter);
            return Ok(books);
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckoutBook([FromBody] BorrowBookDto dto)
        {
            var success = await serviceManager.BookService.BorrowBookAsync(dto);
            if (!success)
                return BadRequest("Could not process book checkout. Verify book availability.");

            return Ok("Book successfully Borrowed.");
        }

        #endregion

        #region Categories Shared Endpoints
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryResultDto>>> GetAllCategories()
        {
            var categories = await serviceManager.CategoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        #endregion

        #region Authors Shared Endpoints
        [HttpGet("authors")]
        public async Task<ActionResult<IEnumerable<AuthorResultDto>>> GetAllAuthors()
        {
            var authors = await serviceManager.AuthorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        #endregion

        #region Publishers Shared Endpoints
        [HttpGet("publishers")]
        public async Task<ActionResult<IEnumerable<PublisherResultDto>>> GetAllPublishers()
        {
            var publishers = await serviceManager.PublisherService.GetAllPublishersAsync();
            return Ok(publishers);
        }
        #endregion

        #region Languages Shared Endpoints

        [HttpGet("languages")]
        public async Task<ActionResult<IEnumerable<PublisherResultDto>>> GetAllLanguages()
        {
            var Languages = await serviceManager.LanguageService.GetAllLanguagesAsync();
            if (Languages == null)
                return NotFound($"No Languages found.");
            return Ok(Languages);
        }
        #endregion

    }
}
