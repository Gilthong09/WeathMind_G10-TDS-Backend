namespace WealthMind.Core.Application.ViewModels.FinancialGoal
{
    public class FinancialGoalViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string ProductType { get; set; }
        public Dictionary<string, object> AdditionalData { get; set; }
    }
}
