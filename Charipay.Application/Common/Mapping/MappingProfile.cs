using AutoMapper;
using Charipay.Application.Commands;
using Charipay.Application.DTOs.Users;
using Charipay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<CreateUserRequest, User>().ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            //CreateMap<User, CreateUserRequest>().ReverseMap().ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password)); ;

        }
    }
}
