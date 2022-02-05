using System;
using System.Collections.Generic;

namespace Expense_Tracking.Models
{
    public partial class ItemList
    {
        public int ItemlId { get; set; }
        public int? ItemId { get; set; }
        public int? ExpId { get; set; }

        public virtual Expenses Exp { get; set; }
        public virtual Items Item { get; set; }
    }
}
