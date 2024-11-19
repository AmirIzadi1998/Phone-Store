﻿using Application.CQRS.NotificationsCQRS;
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
        }
    }
}
