using AutoMapper;
using WealthMind.Core.Application.DTOs.Account;
using WealthMind.Core.Application.ViewModels.CategoryV;
using WealthMind.Core.Application.ViewModels.ChatbotMessage;
using WealthMind.Core.Application.ViewModels.ChatbotSession;
using WealthMind.Core.Application.ViewModels.FinancialGoal;
using WealthMind.Core.Application.ViewModels.Product;
using WealthMind.Core.Application.ViewModels.RecommendationV;
using WealthMind.Core.Application.ViewModels.ReportV;
using WealthMind.Core.Application.ViewModels.TransactionV;
using WealthMind.Core.Application.ViewModels.User;
using WealthMind.Core.Domain.Entities;

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

            #region ChatbotMessage
            CreateMap<SaveChatbotMessageViewModel, ChatbotMessage>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SessionId, opt => opt.MapFrom(src => src.SessionId))
                .ForMember(dest => dest.UserMessage, opt => opt.MapFrom(src => src.UserMessage))
                .ForMember(dest => dest.BotResponse, opt => opt.MapFrom(src => src.BotResponse))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp))
                .ForMember(dest => dest.Session, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Created, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore());

            CreateMap<ChatbotMessage, ChatbotMessageViewModel>()
                .ForMember(dest => dest.SessionId, opt => opt.MapFrom(src => src.SessionId))
                .ForMember(dest => dest.UserMessage, opt => opt.MapFrom(src => src.UserMessage))
                .ForMember(dest => dest.BotResponse, opt => opt.MapFrom(src => src.BotResponse))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp));
            #endregion

            #region ChatbotSession

            CreateMap<ChatbotSession, ChatbotSessionViewModel>();

            CreateMap<SaveChatbotSessionViewModel, ChatbotSession>()
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore());
            #endregion

            #region Product
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
                .ForMember(dest => dest.AdditionalData, opt => opt.MapFrom(src => new Dictionary<string, object>()));

            CreateMap<Saving, ProductViewModel>()
                .IncludeBase<Product, ProductViewModel>()
                .ForMember(dest => dest.AdditionalData, opt => opt.MapFrom(src => new Dictionary<string, object>
                {
                    { "FinancialGoals", src.FinancialGoals ?? new List<FinancialGoal>() }
                }));
            #endregion
            
            #region Transactions
            CreateMap<Transaction, TransactionViewModel>()
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<SaveTransactionViewModel, TransactionViewModel>()
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<SaveTransactionViewModel, Transaction>().ReverseMap()
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region Categories
            CreateMap<Category, CategoryViewModel>()
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<SaveCategoryViewModel, CategoryViewModel>()
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<SaveCategoryViewModel, Category>().ReverseMap()
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region Recommendations
            CreateMap<Recommendation, RecommendationViewModel>()
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<SaveRecommendationViewModel, RecommendationViewModel>()
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<SaveRecommendationViewModel, Recommendation>().ReverseMap()
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region Reports
            CreateMap<Report, ReportViewModel>()
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<SaveReportViewModel, ReportViewModel>()
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<SaveReportViewModel, Report>().ReverseMap()
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion


            #region FinancialGoal
            CreateMap<FinancialGoal, FinancialGoalViewModel>()
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<SaveFinancialGoalViewModel, FinancialGoalViewModel>()
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<SaveFinancialGoalViewModel, FinancialGoal>().ReverseMap()
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion
        }
    }
}
