using Assignment.API.Resources;
using AutoMapper;

namespace Assignment.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Person, RightPersonResource>();
            CreateMap<Person, LeftPersonResource>();
            CreateMap<DiffResult, PeopleDifferenceResource>();
        }
    }
}