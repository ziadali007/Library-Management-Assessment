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
    public class CategoryService(IMapper mapper,ILibraryUnitOfWork unitOfWork) : ICategoryService
    {

        public async Task<IEnumerable<CategoryResultDto>> GetAllCategoriesAsync()
        {
            var categoryRepository =await unitOfWork.GetRepository<Category>().GetAllAsync();
            var result = mapper.Map<IEnumerable<CategoryResultDto>>(categoryRepository);
            return result;

        }
        public async Task<bool> AddCategoryAsync(AddCategoryDto category)
        {
            var categoryEntity = mapper.Map<Category>(category);
            await unitOfWork.GetRepository<Category>().AddAsync(categoryEntity);
            var changes = await unitOfWork.SaveChangesAsync();
            if (changes == 0)
            {
                throw new Exception("No changes were made to the database.");
            }
            return true;
        }

        public async Task<bool> UpdateCategoryAsync(AddCategoryDto category)
        {
            var categoryRepository = await unitOfWork.GetRepository<Category>().GetByIdAsync(category.Id);
            if (categoryRepository == null)
            {
                throw new KeyNotFoundException($"Category with ID {category.Id} not found.");
            }

            mapper.Map(category, categoryRepository);
            var changes = await unitOfWork.SaveChangesAsync();
            if (changes == 0)
            {
                throw new Exception("No changes were made to the database.");
            }
            return true;

        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category= await unitOfWork.GetRepository<Category>().GetByIdAsync(categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
            }

            unitOfWork.GetRepository<Category>().Delete(category);

            var changes = await unitOfWork.SaveChangesAsync();
            if (changes == 0)
            {
                throw new Exception("No changes were made to the database.");
            }
            return true;
        }
    }
}
