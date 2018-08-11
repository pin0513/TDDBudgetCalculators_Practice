using System;

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

                if (budget.LastDay < period.End)
                {
                    return OverlappingAmount(period, budget);
                }

                return 1 * period.DaysInPeriod();
            }
            return 0;
        }

        private static decimal OverlappingAmount(Period period, Budget budget)
        {
            return 1 * period.EffectiveDays(budget);
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

        public int EffectiveDays(Budget budget)
        {
            return ((budget.LastDay - Start).Days + 1);
        }
    }
}