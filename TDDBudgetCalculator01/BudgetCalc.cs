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

            var budget = budgets[0];
            if (budget != null)
            {
                if (budget.FirstDay > period.End)
                {
                    return 0;
                }

                if (budget.LastDay < period.Start)
                {
                    return 0;
                }

                var dailyAmount = budget.Amount / ((budget.LastDay - budget.FirstDay).Days);
                return dailyAmount * period.EffectiveDays(budget);

                return 1 * period.EffectiveDays(budget);
            }
            return 0;
        }
    }
}