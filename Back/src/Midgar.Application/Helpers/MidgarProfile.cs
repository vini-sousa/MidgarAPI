using AutoMapper;
using Midgar.Application.DTOs;
using Midgar.Domain.Entities;

namespace Midgar.Application.Helpers
{
    public class MidgarProfile : Profile
    {
        public MidgarProfile() 
        {
            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<Lote, LoteDTO>().ReverseMap();
            CreateMap<SocialMedia, SocialMediaDTO>().ReverseMap();
            CreateMap<Speaker, SpeakerDTO>().ReverseMap();
        }
    }
}