﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Dtos;
using WebshopServer.Models;

namespace WebshopServer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponseDto>();
            CreateMap<RegisterRequestDto, User>();
            CreateMap<UserRequestDto, User>();
            CreateMap<User, VerificationResponseDto>();
            CreateMap<VerificationRequestDto, User>();
            CreateMap<Article, ArticleResponseDto>();
            CreateMap<Article, DeleteResponseDto>();
            CreateMap<ArticleRequestDto, Article>();
            CreateMap<Order, OrderResponseDto>();
            CreateMap<Order, DeleteResponseDto>();
            CreateMap<OrderRequestDto, Order>();
        }
    }
}
