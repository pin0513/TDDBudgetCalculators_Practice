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

        public decimal TotalAmount(DateTime start, DateTime end)
        {
            var budgets = _budgetRepo.GetAll();

            var period = new Period(start, end);

            var budget = budgets.FirstOrDefault(b =>
                period.Start.ToString("yyyyMM") == b.YearMonth && period.End.ToString("yyyyMM") == b.YearMonth);
            if (budget != null)
            {
                var daysInPeriod = (period.End - period.Start).Days + 1;
                return 1 * daysInPeriod;
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
    }
}