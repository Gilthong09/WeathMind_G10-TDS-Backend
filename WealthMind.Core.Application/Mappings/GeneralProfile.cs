using AutoMapper;
using WealthMind.Core.Application.DTOs.Account;
using WealthMind.Core.Application.ViewModels.User;

namespace RoyalState.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region User
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(dest => dest.HasError, option => option.Ignore())
                .ForMember(dest => dest.Error, option => option.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(dest => dest.HasError, option => option.Ignore())
                .ForMember(dest => dest.Error, option => option.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, RegisterDTO>()
             .ReverseMap()
             .ForMember(dest => dest.Role, option => option.Ignore());

            CreateMap<UserViewModel, UserDTO>()
                .ReverseMap();

            CreateMap<UpdateUserRequest, SaveUserViewModel>()
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.HasError, opt => opt.Ignore())
                .ForMember(dest => dest.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterDTO, SaveUserViewModel>()
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.HasError, opt => opt.Ignore())
            .ForMember(dest => dest.Error, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ReverseMap();
            #endregion

        }
    }
}
