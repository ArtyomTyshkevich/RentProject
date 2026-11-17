using AutoMapper;
using User.BLL.DTOs.Review;
using UserService.BLL.DTOs;
using UserService.DAL.Entities;

namespace UserService.BLL.Mapping.Profiles
{
    public class ProsMappingProfile : Profile
    {
        public ProsMappingProfile()
        {
            CreateMap<Pros, ProsDto>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProsType,
                    opt => opt.MapFrom(src => src.ProsType));

            CreateMap<ProsDto, Pros>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProsType,
                    opt => opt.MapFrom(src => src.ProsType));
        }
    }
}