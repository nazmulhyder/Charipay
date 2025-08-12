using AutoMapper;
using Charipay.Application.Commands.Users;
using Charipay.Application.DTOs.Users;
using Charipay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<User, UpdateUserCommand>();
            CreateMap<UpdateUserCommand, User>().ReverseMap();

            CreateMap<UserDto, UpdateUserCommand>();
            CreateMap<UpdateUserCommand, UserDto>().ReverseMap();

        }
    }
}
