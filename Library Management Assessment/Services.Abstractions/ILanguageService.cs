using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ILanguageService
    {
        Task<IEnumerable<LanguageResultDto>> GetAllLanguagesAsync();
        Task<LanguageResultDto> GetLanguageByIdAsync(int id);
        Task<AddLanguageDto> AddLanguageAsync(AddLanguageDto languageDto);
        Task<LanguageResultDto> UpdateLanguageAsync(AddLanguageDto languageDto);
        Task<bool> DeleteLanguageAsync(int id);
    }
}
