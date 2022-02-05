using System;
using System.Collections.Generic;

namespace Expense_Tracking.Models
{
    public partial class Users
    {
        public Users()
        {
            Expenses = new HashSet<Expenses>();
        }

        public int Userid { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public long Phoneno { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Expenses> Expenses { get; set; }
    }
}
