using WealthMind.Core.Application.ViewModels.CreditCard;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface ICreditCardService : IGenericService<SaveCreditCardViewModel, CreditCardViewModel, CreditCard>
    {

    }
}
