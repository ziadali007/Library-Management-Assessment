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
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookResultDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(a => a.Name).ToList()))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(c => c.Name).ToList()))               
            .ReverseMap();
            CreateMap<Book,AddBookDto>().ReverseMap();
            CreateMap<Book,UpdateBookDto>().ReverseMap();
        }
    }
}
