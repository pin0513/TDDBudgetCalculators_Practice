using System;
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

            var budget = budgets.FirstOrDefault(b =>
                period.Start.ToString("yyyyMM") == b.YearMonth && period.End.ToString("yyyyMM") == b.YearMonth);
            if (budget != null)
            {
                return 1 * period.DaysInPeriod();
            }
            return 0;
        }
    }

    public class Period
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public int DaysInPeriod()
        {
            return (End - Start).Days + 1;
        }
    }
}