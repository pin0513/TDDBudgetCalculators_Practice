using System;

namespace TDDBudgetCalculator01
{
    public class Budget
    {
        public string YearMonth { get; set; }
        public int Amount { get; set; }

        public DateTime LastDay => new DateTime(FirstDay.Year, FirstDay.Month, DateTime.DaysInMonth(FirstDay.Year, FirstDay.Month));

        public DateTime FirstDay => DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null);

        public int DaysInMonth()
        {
            return ((LastDay - FirstDay).Days);
        }

        public int DailyAmount()
        {
            return Amount / DaysInMonth();
        }

        public int CalcAmount(Period period)
        {
            return period.IsBudgetMonth(this) ? DailyAmount() * period.EffectiveDays(this) : 0;
        }
    }
}