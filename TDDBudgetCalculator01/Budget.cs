using System;

namespace TDDBudgetCalculator01
{
    public class Budget
    {
        public string YearMonth { get; set; }
        public int Amount { get; set; }

        public DateTime LastDay
        {
            get
            {
                return new DateTime(FirstDay().Year, FirstDay().Month,
                    DateTime.DaysInMonth(FirstDay().Year, FirstDay().Month));
            }
        }

        private DateTime FirstDay()
        {
            return DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null);
        }
    }
}