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
            return _budgetRepo.GetAll().Sum(b => b.CalcAmount(period));
            //var budgets = _budgetRepo.GetAll();

            //var amount = 0m;
            //foreach (var budget in budgets)
            //{
            //    amount += budget.CalcAmount(period);
            //}

            //return amount;
        }
    }
}