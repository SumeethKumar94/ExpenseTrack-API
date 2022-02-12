using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracking.View_Model
{
    public class UsersView
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long Phoneno { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
