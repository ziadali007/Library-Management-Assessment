using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IPublisherService
    {
        Task<IEnumerable<PublisherResultDto>> GetAllPublishersAsync();
        Task<PublisherResultDto> GetPublisherByIdAsync(int id);
        Task<Shared.AddPublisherDto> AddPublisherAsync(AddPublisherDto publisherDto);
        Task<Shared.PublisherResultDto> UpdatePublisherAsync(AddPublisherDto publisherDto);
        Task<bool> DeletePublisherAsync(int id);
    }
}
