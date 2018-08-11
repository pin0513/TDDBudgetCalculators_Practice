using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;

namespace TDDBudgetCalculator01
{
    /// <summary>
    /// BudgetCalculatorTests 的摘要說明
    /// </summary>
    [TestClass]
    public class BudgetCalculatorTests
    {
        private BudgetCalc _budgetCalc;
        private IBudgetRepo _budgetRepo = Substitute.For<IBudgetRepo>();

        [TestMethod]
        public void NoBudgets()
        {
            AmountShouldBe(new DateTime(2018, 03, 01), new DateTime(2018, 03, 01), 0);
        }

        [TestInitialize]
        public void SetUp()
        {
            _budgetCalc = new BudgetCalc(_budgetRepo);
        }

        [TestMethod]
        public void period_inside_budget_month()
        {
            _budgetRepo.GetAll().Returns(new List<Budget>(){
                new Budget{YearMonth="201803",Amount=31  }
            });
            AmountShouldBe(new DateTime(2018, 03, 01), new DateTime(2018, 03, 01), 1);
        }

        private void AmountShouldBe(DateTime start, DateTime end, int expected)
        {
            var amount = _budgetCalc.TotalAmount(start, end);
            Assert.AreEqual(expected, amount);
        }
    }
}