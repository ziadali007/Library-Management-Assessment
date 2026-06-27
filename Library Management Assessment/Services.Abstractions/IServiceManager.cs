using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IAdminService AdminService { get; }

        IBookService BookService { get; }

        ICategoryService CategoryService { get; }

        IAuthorService AuthorService { get; }

        IPublisherService PublisherService { get; }

        ILanguageService LanguageService { get; }

        IAuthService AuthService { get; }
    }
}
