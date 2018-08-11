namespace TDDBudgetCalculator01
{
    public class BudgetCalc
    {
        private readonly IBudgetRepo _budgetRepo;

        public BudgetCalc(IBudgetRepo budgetRepo)
        {
            _budgetRepo = budgetRepo;
        }

        public decimal TotalAmount(Period period)
        {
            var budgets = _budgetRepo.GetAll();
            var amount = 0m;
            foreach (var budget in budgets)
            {
                amount += period.IsBudgetMonth(budget) ? budget.DailyAmount() * period.EffectiveDays(budget) : 0;
            }

            return amount;
        }
    }
}