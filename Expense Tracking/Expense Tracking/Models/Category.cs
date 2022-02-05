using System;
using System.Collections.Generic;

namespace Expense_Tracking.Models
{
    public partial class Category
    {
        public Category()
        {
            Items = new HashSet<Items>();
        }

        public int CatId { get; set; }
        public string CatName { get; set; }

       public virtual ICollection<Items> Items { get; set; }
    }
}
