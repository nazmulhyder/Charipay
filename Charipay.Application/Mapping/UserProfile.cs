using AutoMapper;
using Charipay.Application.Commands.Campaigns;
using Charipay.Application.Commands.Charities;
using Charipay.Application.Commands.Users;
using Charipay.Application.DTOs.Admin.Dashboard.Users;
using Charipay.Application.DTOs.Campaigns;
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

            CreateMap<Charity, CharityDto>();
            CreateMap<CharityDto, Charity>().ReverseMap();

            CreateMap<Charity, UpdateCharityCommand>();
            CreateMap<UpdateCharityCommand, Charity>();

            #endregion Charity

            #region Campaign
            CreateMap<CreateCampaignCommand, CampaignDto>();
            CreateMap<CampaignDto, CreateCampaignCommand>().ReverseMap();
            CreateMap<Campaign, CampaignDto>();
            CreateMap<CampaignDto, Campaign>().ReverseMap();
            CreateMap<CreateCampaignCommand, Campaign>();
            CreateMap<Campaign, CreateCampaignCommand>().ReverseMap();
            #endregion

            #region Admin dashboard : User List
            CreateMap<User, AdminUserListDto>()
                .ForMember(dest => dest.Role, 
                opt => opt.MapFrom(src=>src.UserRoles.Select(x=>x.Role.Name).FirstOrDefault()));
            #endregion


        }
    }
}
