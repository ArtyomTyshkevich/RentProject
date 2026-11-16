using AutoMapper;
using User.BLL.DTOs.Review;
using UserService.DAL.Entities;

namespace UserService.BLL.Mapping.Profiles
{
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Score,
                    opt => opt.MapFrom(src => src.Score))
                .ForMember(dest => dest.Text,
                    opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Pros,
                    opt => opt.MapFrom(src => src.Pros))
                .ForMember(dest => dest.Reviewer,
                    opt => opt.MapFrom(src => src.Reviewer))
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => src.UpdatedAt));

            CreateMap<ReviewDto, Review>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Score,
                    opt => opt.MapFrom(src => src.Score))
                .ForMember(dest => dest.Text,
                    opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Pros,
                    opt => opt.MapFrom(src => src.Pros))
                .ForMember(dest => dest.Reviewer,
                    opt => opt.MapFrom(src => src.Reviewer))
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => src.UpdatedAt));
        }
    }
}