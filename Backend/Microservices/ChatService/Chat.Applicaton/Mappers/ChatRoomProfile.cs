using AutoMapper;
using Chat.Application.DTOs;
using Chat.Domain.Models.Entites;

namespace Chat.Applicaton.Mappers
{
    public class ChatRoomProfile : Profile
    {
        public ChatRoomProfile()
        {
            CreateMap<ChatRoom, ChatRoomDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator))
                .ForMember(dest => dest.Members, opt => opt.MapFrom(src => new List<Guid>(src.Members)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ChatRoomType, opt => opt.MapFrom(src => src.ChatRoomType));

            CreateMap<ChatRoomDto, ChatRoom>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator))
                .ForMember(dest => dest.Members, opt => opt.MapFrom(src => new List<Guid>(src.Members)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ChatRoomType, opt => opt.MapFrom(src => src.ChatRoomType));
        }
    }
}