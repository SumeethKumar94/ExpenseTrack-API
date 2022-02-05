using System;
using System.Collections.Generic;

namespace Expense_Tracking.Models
{
    public partial class Expenses
    {
        public int ExpId { get; set; }
        public int? Userid { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public int? ItemlId { get; set; }
        public int TotalExp { get; set; }

        public virtual Users User { get; set; }
    }
}
