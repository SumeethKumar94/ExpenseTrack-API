using Expense_Tracking.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracking.Repository
{
    public interface IItemList
    {
        //get itemlists
        Task<List<ItemList>> GetItemLists();

        //add an itemList
        Task<ActionResult<ItemList>> AddItemList(ItemList item);

        //update an itemlist
        Task UpdateItemList(ItemList item);
    }
}
