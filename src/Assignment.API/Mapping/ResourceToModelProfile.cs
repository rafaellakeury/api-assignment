using Assignment.API.Domain.Models;
using Assignment.API.Resources;
using AutoMapper;

namespace Supermarket.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SavePersonResource, LeftPerson>();
            CreateMap<SavePersonResource, RightPerson>();
        }
    }
}