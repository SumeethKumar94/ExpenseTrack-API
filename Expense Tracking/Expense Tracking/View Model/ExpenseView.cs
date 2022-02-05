using Expense_Tracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracking.View_Model
{
    public class ExpenseView
    {
        public int ExpId { get; set; }
        public int? Userid { get; set; }
        public string Username { get; set; }
        public Int64 Phone { get; set; }
 
        public DateTime? ExpenseDate { get; set; }
        public List<ItemView> Itemlist { get; set; }
        public int TotalExp { get; set; }
    }
}
