using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorResultDto>> GetAllAuthorsAsync();
        Task<AuthorResultDto> GetAuthorByIdAsync(int id);
        Task<AddAuthorDto> AddAuthorAsync(Shared.AddAuthorDto authorDto);
        Task<Shared.AuthorResultDto> UpdateAuthorAsync(AddAuthorDto authorDto);
        Task<bool> DeleteAuthorAsync(int id);
    }
}
