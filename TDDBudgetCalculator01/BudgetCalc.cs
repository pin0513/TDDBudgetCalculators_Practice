using System.Linq;

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

            var budget = budgets.FirstOrDefault(b => IsSingleMonth(period, b));
            if (budget != null)
            {
                return budget.DailyAmount() * period.EffectiveDays(budget);
            }
            return 0;
        }

        private static bool IsSingleMonth(Period period, Budget budget)
        {
            return budget.YearMonth == period.Start.ToString("yyyyMM") || budget.YearMonth == period.End.ToString("yyyyMM");
        }
    }
}