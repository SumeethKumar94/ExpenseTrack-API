using Expense_Tracking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracking.Repository
{
    public class ItemListsClass : IItemList
    {

        private readonly ExpenseTrackingContext _context;

        //constructor based dependency injection
        public ItemListsClass(ExpenseTrackingContext context)
        {
            _context = context;
        }

        [HttpGet]
        //get itemlists
        #region get Itemslists
        public async Task<List<ItemList>> GetItemLists()
        {
            if (_context != null)
            {
                return await _context.ItemList.Include(i => i.Item).ToListAsync();

            }
            return null;
        }
        #endregion

        [HttpPost]
        //add new item lists
        #region Post itemlist
        public async Task<ActionResult<ItemList>> AddItemList(ItemList item)
        {
            if (_context != null)
            {

                await _context.ItemList.AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }
            return null;
        }



        #endregion

        [HttpPut]
        //update a list
        #region update
        public async Task UpdateItemList(ItemList item)
        {
            if (_context != null)
            {
                _context.Entry(item).State = EntityState.Modified;
                _context.ItemList.Update(item);
                await _context.SaveChangesAsync();
            }

        }
        #endregion
    }
}
