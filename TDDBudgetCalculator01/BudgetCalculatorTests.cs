﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestMethod]
        public void no_overlap_period_after_budget_LastDay()
        {
            _budgetRepo.GetAll().Returns(new List<Budget>(){
                new Budget{YearMonth="201803",Amount=31  }
            });
            AmountShouldBe(new DateTime(2018, 4, 1), new DateTime(2018, 4, 1), 0);
        }

        [TestMethod]
        public void no_overlap_period_before_budget_FirstDay()
        {
            _budgetRepo.GetAll().Returns(new List<Budget>(){
                new Budget{YearMonth="201803",Amount=31  }
            });
            AmountShouldBe(new DateTime(2018, 2, 1), new DateTime(2018, 2, 1), 0);
        }

        [TestMethod]
        public void overlap_period_budget_LastDay()
        {
            _budgetRepo.GetAll().Returns(new List<Budget>(){
                new Budget{YearMonth="201803",Amount=31  }
            });
            AmountShouldBe(new DateTime(2018, 3, 31), new DateTime(2018, 4, 1), 1);
        }

        [TestMethod]
        public void overlap_period_budget_FirstDay()
        {
            _budgetRepo.GetAll().Returns(new List<Budget>(){
                new Budget{YearMonth="201803",Amount=31  }
            });
            AmountShouldBe(new DateTime(2018, 2, 28), new DateTime(2018, 3, 1), 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void inValidDate()
        {
            _budgetRepo.GetAll().Returns(new List<Budget>(){
                new Budget{YearMonth="201803",Amount=31  }
            });
            AmountShouldBe(new DateTime(2018, 3, 1), new DateTime(2018, 2, 1), 1);
        }

        [TestMethod]
        public void DailyAmount()
        {
            _budgetRepo.GetAll().Returns(new List<Budget>(){
                new Budget{YearMonth="201803",Amount=310  },
            });
            AmountShouldBe(new DateTime(2018, 3, 1), new DateTime(2018, 3, 2), 20);
        }

        [TestMethod]
        public void MultipleBudgets()
        {
            _budgetRepo.GetAll().Returns(new List<Budget>(){
                new Budget{YearMonth="201803",Amount=310  },
                new Budget{YearMonth="201804",Amount=30  },
            });
            AmountShouldBe(new DateTime(2018, 3, 31), new DateTime(2018, 4, 1), 11);
        }

        private void AmountShouldBe(DateTime start, DateTime end, int expected)
        {
            var amount = _budgetCalc.TotalAmount(new Period(start, end));
            Assert.AreEqual(expected, amount);
        }
    }
}