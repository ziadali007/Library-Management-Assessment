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
    public class BaseController(IServiceManager serviceManager) : ControllerBase
    {
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

    }
}
