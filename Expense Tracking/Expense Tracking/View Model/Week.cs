using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracking.View_Model
{
    public class Week
    {
        public int Weekno { get; set; }
        public List<ExpenseReport>WeekList { get; set; }

    }
}
