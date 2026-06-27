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
    public class LanguageService(IMapper mapper,ILibraryUnitOfWork unitOfWork) : ILanguageService
    {
        public async Task<IEnumerable<LanguageResultDto>> GetAllLanguagesAsync()
        {
            var languages =await unitOfWork.GetRepository<Language>().GetAllAsync();
            return mapper.Map<IEnumerable<LanguageResultDto>>(languages);
        }

        public async Task<LanguageResultDto> GetLanguageByIdAsync(int id)
        {
            var language =await unitOfWork.GetRepository<Language>().GetByIdAsync(id);
            if (language == null)
                return null;
            return mapper.Map<LanguageResultDto>(language);

        }

        public async Task<AddLanguageDto> AddLanguageAsync(AddLanguageDto languageDto)
        {
            var language = mapper.Map<Language>(languageDto);
            await unitOfWork.GetRepository<Language>().AddAsync(language);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<AddLanguageDto>(language);
        }

        public async Task<LanguageResultDto> UpdateLanguageAsync(AddLanguageDto languageDto)
        {
            var language= await unitOfWork.GetRepository<Language>().GetByIdAsync(languageDto.Id);
            if (language == null)
                return null;
            mapper.Map(languageDto, language);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<LanguageResultDto>(language);
        }
        public async Task<bool> DeleteLanguageAsync(int id)
        {
            var language =await unitOfWork.GetRepository<Language>().GetByIdAsync(id);
            if (language == null)
                return false;
            unitOfWork.GetRepository<Language>().Delete(language);
            await unitOfWork.SaveChangesAsync();
            return true;
        }


    }
}
