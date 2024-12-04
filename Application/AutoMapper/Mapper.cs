using Application.CQRS.NotificationsCQRS;
using Application.CQRS.PhoneProductCQRS.Command;
using Application.CQRS.PhoneProductCQRS.Query;
using AutoMapper;
using Infrastructure.Dto;
using Infrastructure.Entities;

namespace Application.AutoMapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<PhoneDto , PhoneProduct>().ReverseMap();
            CreateMap<PhoneProduct, GetQueryResponse>().ReverseMap();
            CreateMap<AddRefreshTokenNotification, UserRefreshToken>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserRefreshToken, UserRefreshTokenDto>().ReverseMap();
            CreateMap<PhoneProduct, GetAllQueryRequest>().ReverseMap();
            CreateMap<PhoneProduct, UpdateCommandResponse>().ReverseMap();
            CreateMap<PhoneProduct, UpdateCommand>().ReverseMap();
            CreateMap<PhoneProduct, GetByNameResponse>().ReverseMap();
        }
    }
}
