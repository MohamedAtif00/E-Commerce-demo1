using AutoMapper;
using E_Commerce.Application.Authentication;
using E_Commerce.Contract.Authentication.Register.Request;

namespace E_Commerce.Api.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {

            CreateMap<LoginRequest,LoginDto>();
        }
    }
}
