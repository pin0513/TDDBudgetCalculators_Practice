using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TDDBudgetCalculator01
{
    /// <summary>
    /// BudgetCalculatorTests 的摘要說明
    /// </summary>
    [TestClass]
    public class BudgetCalculatorTests
    {
        [TestMethod]
        public void NoBudgets()
        {
            var budgetCalc = new BudgetCalc();
            var start = new DateTime(2018, 03, 01);
            var end = new DateTime(2018, 03, 01);
            var amount = budgetCalc.TotalAmount(start, end);
            Assert.AreEqual(0, amount);
        }
    }
}