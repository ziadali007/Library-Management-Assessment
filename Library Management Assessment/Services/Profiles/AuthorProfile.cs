using AutoMapper;
using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorResultDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(b => b.Title)));
            CreateMap<AddAuthorDto, Author>()
                .ForMember(dest => dest.Books, opt => opt.Ignore());
        }
    }
}
