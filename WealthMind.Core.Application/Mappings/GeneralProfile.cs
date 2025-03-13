using AutoMapper;
using WealthMind.Core.Application.DTOs.Account;
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

            //#region Cash
            //CreateMap<Cash, CashViewModel>()
            //.ForMember(x => x.HasError, opt => opt.Ignore())
            //.ForMember(x => x.Error, opt => opt.Ignore())
            //.ReverseMap()
            //.ForMember(x => x.CreatedBy, opt => opt.Ignore())
            //.ForMember(x => x.LastModified, opt => opt.Ignore())
            //.ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            //CreateMap<SaveCashViewModel, CashViewModel>()
            //.ForMember(x => x.HasError, opt => opt.Ignore())
            //.ForMember(x => x.Error, opt => opt.Ignore())
            //.ReverseMap();

            //CreateMap<SaveCashViewModel, Cash>().ReverseMap()
            //.ForMember(x => x.HasError, opt => opt.Ignore())
            //.ForMember(x => x.Error, opt => opt.Ignore())
            //.ReverseMap()
            //.ForMember(x => x.CreatedBy, opt => opt.Ignore())
            //.ForMember(x => x.LastModified, opt => opt.Ignore())
            //.ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            //#endregion

            //#region CreditCard
            //CreateMap<CreditCard, CreditCardViewModel>()
            //.ForMember(x => x.HasError, opt => opt.Ignore())
            //.ForMember(x => x.Error, opt => opt.Ignore())
            //.ReverseMap()
            //.ForMember(x => x.CreatedBy, opt => opt.Ignore())
            //.ForMember(x => x.LastModified, opt => opt.Ignore())
            //.ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            //CreateMap<SaveCreditCardViewModel, CreditCardViewModel>()
            //.ForMember(x => x.HasError, opt => opt.Ignore())
            //.ForMember(x => x.Error, opt => opt.Ignore())
            //.ReverseMap();

            //CreateMap<SaveCreditCardViewModel, CreditCard>().ReverseMap()
            //.ForMember(x => x.HasError, opt => opt.Ignore())
            //.ForMember(x => x.Error, opt => opt.Ignore())
            //.ReverseMap()
            //.ForMember(x => x.CreatedBy, opt => opt.Ignore())
            //.ForMember(x => x.LastModified, opt => opt.Ignore())
            //.ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            //#endregion

            //#region Loan
            //CreateMap<Loan, LoanViewModel>()
            //.ForMember(x => x.HasError, opt => opt.Ignore())
            //.ForMember(x => x.Error, opt => opt.Ignore())
            //.ReverseMap()
            //.ForMember(x => x.CreatedBy, opt => opt.Ignore())
            //.ForMember(x => x.LastModified, opt => opt.Ignore())
            //.ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            //CreateMap<SaveLoanViewModel, LoanViewModel>()
            //.ForMember(x => x.HasError, opt => opt.Ignore())
            //.ForMember(x => x.Error, opt => opt.Ignore())
            //.ReverseMap();

            //CreateMap<SaveLoanViewModel, Loan>().ReverseMap()
            //.ForMember(x => x.HasError, opt => opt.Ignore())
            //.ForMember(x => x.Error, opt => opt.Ignore())
            //.ReverseMap()
            //.ForMember(x => x.CreatedBy, opt => opt.Ignore())
            //.ForMember(x => x.LastModified, opt => opt.Ignore())
            //.ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            //#endregion

            //#region Saving
            //CreateMap<Saving, SavingViewModel>()
            //.ForMember(x => x.HasError, opt => opt.Ignore())
            //.ForMember(x => x.Error, opt => opt.Ignore())
            //.ReverseMap()
            //.ForMember(x => x.CreatedBy, opt => opt.Ignore())
            //.ForMember(x => x.LastModified, opt => opt.Ignore())
            //.ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            //CreateMap<SaveSavingViewModel, SavingViewModel>()
            //.ForMember(x => x.HasError, opt => opt.Ignore())
            //.ForMember(x => x.Error, opt => opt.Ignore())
            //.ReverseMap();

            //CreateMap<SaveSavingViewModel, Saving>().ReverseMap()
            //.ForMember(x => x.HasError, opt => opt.Ignore())
            //.ForMember(x => x.Error, opt => opt.Ignore())
            //.ReverseMap()
            //.ForMember(x => x.CreatedBy, opt => opt.Ignore())
            //.ForMember(x => x.LastModified, opt => opt.Ignore())
            //.ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            //#endregion

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

        }
    }
}
