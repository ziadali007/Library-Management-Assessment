using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthorService(IMapper mapper,ILibraryUnitOfWork unitOfWork) : IAuthorService
    {
        public async Task<IEnumerable<AuthorResultDto>> GetAllAuthorsAsync()
        {
           var authors=await unitOfWork.GetRepository<Author>().GetAllAsync();
            return mapper.Map<IEnumerable<AuthorResultDto>>(authors);

        }

        public async Task<AuthorResultDto> GetAuthorByIdAsync(int id)
        {
            var author =await unitOfWork.GetRepository<Author>().GetByIdAsync(id);
            if (author == null)
                return null;
            return mapper.Map<AuthorResultDto>(author);
        }
        public async Task<AddAuthorDto> AddAuthorAsync(AddAuthorDto authorDto)
        {
            var author = mapper.Map<Author>(authorDto);
            await unitOfWork.GetRepository<Author>().AddAsync(author);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<AddAuthorDto>(author);
        }

        public async Task<AuthorResultDto> UpdateAuthorAsync(AddAuthorDto authorDto)
        {
           var author = await unitOfWork.GetRepository<Author>().GetByIdAsync(authorDto.Id);
            if (author == null)
                return null;
            mapper.Map(authorDto, author);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<AuthorResultDto>(author);
        }
        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await unitOfWork.GetRepository<Author>().GetByIdAsync(id);
            if (author == null)
                return false;
            unitOfWork.GetRepository<Author>().Delete(author);
            await unitOfWork.SaveChangesAsync();
            return true;

        }

    }
}
