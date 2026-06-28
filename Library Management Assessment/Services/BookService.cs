using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookService(ILibraryUnitOfWork unitOfWork, IMapper mapper) : IBookService
    {

        public async Task<IEnumerable<BookResultDto>> SearchBooksAsync(BookSearchFilter filter)
        {
            var bookRepository = unitOfWork.GetRepository<Book>();
            IQueryable<Book> query = bookRepository.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                query = query.Where(b => b.Title.Contains(filter.Title));
            }

            if (!string.IsNullOrWhiteSpace(filter.Author))
            {
                query = query.Where(b => b.Authors.Any(auth => auth.Name.Contains(filter.Author)));
            }

            if (!string.IsNullOrWhiteSpace(filter.Category))
            {
                query = query.Where(b => b.Categories.Any(cat => cat.Name.Contains(filter.Category)));
            }

            query = query.Include(b => b.Categories)
                         .Include(b => b.Authors);

            var books = await query.AsNoTracking().ToListAsync();

           return mapper.Map<IEnumerable<BookResultDto>>(books);
        }

        public  async Task<IEnumerable<BookResultDto>> GetAllBooksAsync()
        {
           var bookRepository =await unitOfWork.GetRepository<Book>().GetAllAsync();
           var result = mapper.Map<IEnumerable<BookResultDto>>(bookRepository);
           return result;
        }

        public async Task<BookResultDto> GetBookByIdAsync(int bookId)
        {
            var bookRepository =await unitOfWork.GetRepository<Book>().GetByIdAsync(bookId);
            if (bookRepository == null)
            {
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }
            var result = mapper.Map<BookResultDto>(bookRepository);
            return result;
        }
        public async Task<bool> AddBookAsync(AddBookDto bookDto)
        {
           var result= mapper.Map<Book>(bookDto);
           await unitOfWork.GetRepository<Book>().AddAsync(result);
            var check = await unitOfWork.SaveChangesAsync();
            if (check == 0)
            {
                throw new Exception("No changes were made to the database.");
            }

            return true;
        }

        public async Task<bool> UpdateBookAsync(UpdateBookDto bookDto)
        {
           var existingBook = await unitOfWork.GetRepository<Book>().GetByIdAsync(bookDto.Id);
            if (existingBook == null)
            {
                throw new KeyNotFoundException($"Book with ID {bookDto.Id} not found.");
            }

           var book= mapper.Map(bookDto, existingBook);

            unitOfWork.GetRepository<Book>().Update(book);

            var result=await unitOfWork.SaveChangesAsync();
            if (result == 0)
            {
                throw new Exception("No changes were made to the book.");
            }

            return true;
        }
        public async Task<bool> DeleteBookAsync(int bookId)
        {
           var existingBook =await unitOfWork.GetRepository<Book>().GetByIdAsync(bookId);
            if (existingBook == null)
            {
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }
            unitOfWork.GetRepository<Book>().Delete(existingBook);
            var result =await unitOfWork.SaveChangesAsync();
            if (result == 0)
            {
                throw new Exception("No changes were made to the book.");
            }
            return true;
        }

        public async Task<bool> BorrowBookAsync(BorrowBookDto dto)
        {
            var bookRepository = unitOfWork.GetRepository<Book>();
            var transactionRepository = unitOfWork.GetRepository<BorrowingTransaction>();

            var book = await bookRepository.GetByIdAsync(dto.BookId);

            if (book == null || book.Status != BookStatus.Available)
            {
                throw new Exception("Book Not Found Or Borrowed"); 
            }

            book.Status = BookStatus.Reserved;

            var transaction = new BorrowingTransaction
            {
                BookId = dto.BookId,
                MemberId = dto.MemberId,
                IssuedById = dto.IssuedById,
                BorrowDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(dto.DaysToBorrow)
            };

            await transactionRepository.AddAsync(transaction);
            var result = await unitOfWork.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> ReturnBookAsync(ReturnBookDto dto)
        {
            var bookRepository = unitOfWork.GetRepository<Book>();
            var transactionRepository = unitOfWork.GetRepository<BorrowingTransaction>();

            var transaction = await transactionRepository.AsQueryable()
                .FirstOrDefaultAsync(t => t.Id == dto.TransactionId && t.ReturnDate == null);

            if (transaction == null)
            {
                throw new Exception("Transaction not found or book already returned");
            }

            var book = await bookRepository.AsQueryable()
                .FirstOrDefaultAsync(b => b.Id == transaction.BookId);

            if (book == null)
            {
                throw new Exception("Book Not Found");
            }

            transaction.ReturnDate = DateTime.UtcNow;
            transaction.ProcessedById = dto.ProcessedById;

            book.Status = BookStatus.Available;

            var result = await unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<BookResultDto>> GetBooksByStatusAsync(BookStatus status)
        {
            var books= await unitOfWork.GetRepository<Book>().AsQueryable()
            .Include(b => b.Language)
            .Include(b => b.Authors)
            .Include(b => b.Categories)
            .Where(b => b.Status == status)
            .ToListAsync();

            return mapper.Map<IEnumerable<BookResultDto>>(books);


        }
    }
}
