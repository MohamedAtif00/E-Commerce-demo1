using AutoMapper;
using E_Commerce.Application.Authentication;
using E_Commerce.Contract.Register.Request;

namespace E_Commerce.Api.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequest, RegisterDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                //.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        }
    }
}
