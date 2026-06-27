using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResultDto>> GetAllCategoriesAsync();

        Task<bool> AddCategoryAsync(AddCategoryDto category);

        Task<bool> UpdateCategoryAsync(AddCategoryDto category);

        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}
