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
    public class LanguageProfile : Profile
    {
        public LanguageProfile() 
        {
            CreateMap<Language, LanguageResultDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(b => b.Title)));
            CreateMap<AddLanguageDto, Language>()
                .ForMember(dest => dest.Books, opt => opt.Ignore()).ReverseMap();



        }

    }
}
