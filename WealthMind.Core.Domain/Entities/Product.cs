using WealthMind.Core.Domain.Common;

namespace WealthMind.Core.Domain.Entities
{
    public abstract class Product : AuditableBaseEntity
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public int Status { get; set; }

        public string ProductType { get; private set; } // Discriminador automático de EF Core

        public void Debit(decimal amount)
        {
            if (Balance < amount) throw new InvalidOperationException("Insufficient funds.");
            Balance -= amount;
        }

        public void Credit(decimal amount)
        {
            Balance += amount;
        }
    }

    public class CreditCard : Product
    {
        public DateTime ExpirationDate { get; set; }
        public decimal? CreditLimit { get; set; }
        public decimal Debt { get; set; }
    }

    public class Loan : Product
    {
        public decimal? InterestRate { get; set; }
        public int TermInMonths { get; set; }
        public decimal Debt { get; set; }
        public decimal Limit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate => StartDate.AddMonths(TermInMonths);
    }

    public class Saving : Product
    {
        public ICollection<FinancialGoal>? FinancialGoals { get; set; }
    }

    public class Investment : Product
    {
        public decimal? ExpectedReturn { get; set; }
        public int DurationInMonths { get; set; }
    }

    public class Cash : Product
    { }
}
