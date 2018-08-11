using System;

namespace TDDBudgetCalculator01
{
    public class Period
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public Period(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new ArgumentException();
            }
            Start = start;
            End = end;
        }

        public int DaysInPeriod()
        {
            return (End - Start).Days + 1;
        }

        public int EffectiveDays(Budget budget)
        {
            if (Start <= budget.LastDay && End > budget.LastDay)
            {
                return ((budget.LastDay - Start).Days + 1);
            }
            if (Start < budget.FirstDay && End <= budget.LastDay)
            {
                return ((End - budget.FirstDay).Days + 1);
            }

            return DaysInPeriod();
        }
    }
}