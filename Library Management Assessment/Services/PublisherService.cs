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
    public class PublisherService(IMapper mapper,ILibraryUnitOfWork unitOfWork) : IPublisherService
    {

        public async Task<IEnumerable<PublisherResultDto>> GetAllPublishersAsync()
        {
           var publishers =await unitOfWork.GetRepository<Publisher>().GetAllAsync();
            return mapper.Map<IEnumerable<PublisherResultDto>>(publishers);
        }

        public async Task<PublisherResultDto> GetPublisherByIdAsync(int id)
        {
           var publisher =await unitOfWork.GetRepository<Publisher>().GetByIdAsync(id);
            if (publisher == null)
                return null;
            return mapper.Map<PublisherResultDto>(publisher);
        }
        public async Task<AddPublisherDto> AddPublisherAsync(AddPublisherDto publisherDto)
        {
            var publisher = mapper.Map<Publisher>(publisherDto);
            await unitOfWork.GetRepository<Publisher>().AddAsync(publisher);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<AddPublisherDto>(publisher);
        }

        public async Task<PublisherResultDto> UpdatePublisherAsync(AddPublisherDto publisherDto)
        {
            var publisher=await unitOfWork.GetRepository<Publisher>().GetByIdAsync(publisherDto.Id);
            if (publisher == null)
                return null;
            mapper.Map(publisherDto, publisher);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<PublisherResultDto>(publisher);
        }
        public async Task<bool> DeletePublisherAsync(int id)
        {
            var publisher =await unitOfWork.GetRepository<Publisher>().GetByIdAsync(id);
            if (publisher == null)
                return false;
            unitOfWork.GetRepository<Publisher>().Delete(publisher);
            await unitOfWork.SaveChangesAsync();
            return true;

        }

    }
}
