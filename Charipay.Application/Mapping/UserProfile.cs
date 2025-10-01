using AutoMapper;
using Charipay.Application.Commands.Charities;
using Charipay.Application.Commands.Users;
using Charipay.Application.DTOs.Charities;
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
            #region User
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<User, UpdateUserCommand>();
            CreateMap<UpdateUserCommand, User>().ReverseMap();

            CreateMap<UserDto, UpdateUserCommand>();
            CreateMap<UpdateUserCommand, UserDto>().ReverseMap();

            CreateMap<User, CreateUserCommand>();
            CreateMap<CreateUserCommand, User>().ReverseMap();

            #endregion User

            #region Charity
            CreateMap<CreateCharityCommand, CharityDto>();
            CreateMap<CharityDto, CreateCharityCommand>().ReverseMap();
            #endregion Charity


        }
    }
}
