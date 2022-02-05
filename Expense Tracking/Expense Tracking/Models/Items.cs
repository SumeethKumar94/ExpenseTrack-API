using System;
using System.Collections.Generic;

namespace Expense_Tracking.Models
{
    public partial class Items
    {
        public Items()
        {
            ItemList = new HashSet<ItemList>();
        }

        public int ItemId { get; set; }
        public int CatId { get; set; }
        public string ItemName { get; set; }
        public int ItemPrice { get; set; }
        public byte[] ItemBill { get; set; }

        public virtual Category Cat { get; set; }
        public virtual ICollection<ItemList> ItemList { get; set; }
    }
}
