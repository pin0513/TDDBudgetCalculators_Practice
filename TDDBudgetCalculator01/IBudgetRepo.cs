using System.Collections.Generic;

namespace TDDBudgetCalculator01
{
    public interface IBudgetRepo
    {
        IList<Budget> GetAll();
    }
}