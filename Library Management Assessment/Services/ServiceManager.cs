using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(UserManager<AppUser> _userManager, ILibraryUnitOfWork unitOfWork, IMapper mapper) : IServiceManager
    {
        public IAdminService AdminService { get; } = new AdminService(_userManager);

        public IBookService BookService { get; } = new BookService(unitOfWork, mapper);

        public ICategoryService CategoryService { get; } = new CategoryService(mapper, unitOfWork);

        public IAuthorService AuthorService { get; } = new AuthorService(mapper, unitOfWork);

        public IPublisherService PublisherService { get; } = new PublisherService(mapper, unitOfWork);

        public ILanguageService LanguageService { get; } = new LanguageService(mapper, unitOfWork);
    }
}
