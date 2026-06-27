using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IBookService
    {
        Task<IEnumerable<BookResultDto>> GetAllBooksAsync();
        Task<BookResultDto> GetBookByIdAsync(int bookId);
        Task<IEnumerable<BookResultDto>> SearchBooksAsync(BookSearchFilter filter);
        Task<bool> AddBookAsync(AddBookDto bookDto);

        Task<bool> UpdateBookAsync(UpdateBookDto bookDto);
        Task<bool> DeleteBookAsync(int bookId);

        Task<bool> BorrowBookAsync(BorrowBookDto dto);
        Task<bool> ReturnBookAsync(ReturnBookDto dto);

    }
}
