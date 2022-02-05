using Expense_Tracking.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracking.Repository
{
   public interface IItems
    {
        Task<List<Items>> GetItems();

        Task<ActionResult<Items>> GetItemByID(int? id);

        Task<ActionResult<Items>> AddItem(Items item);

        Task UpdateItem(Items item);

        Task<int> DeleteItemByID(int? id);
    }
}
