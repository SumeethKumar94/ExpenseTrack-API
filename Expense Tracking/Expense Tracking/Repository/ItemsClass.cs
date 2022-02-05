using Expense_Tracking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracking.Repository
{
    public class ItemsClass:IItems
    {
        private readonly ExpenseTrackingContext _context;

        //constructor based dependency injection
        public ItemsClass(ExpenseTrackingContext context)
        {
            _context = context;
        }

        //get all Items 
        public async Task<List<Items>> GetItems()
        {
            if (_context != null)
            {
                return await _context.Items.ToListAsync();

            }
            return null;
        }

        ////get item by id
        public async Task<ActionResult<Items>> GetItemByID(int? id)
        {
            if (_context != null)
            {
                return await _context.Items.FindAsync(id);    //primary key


            }
            return null;
        }

        //add an item
        public async Task<ActionResult<Items>> AddItem(Items item)
        {
            if (_context != null)
            {

                await _context.Items.AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }
            return null;
        }

        //update an item
        #region update item
        public async Task UpdateItem(Items item)
        {
            if (_context != null)
            {
                _context.Entry(item).State = EntityState.Modified;
                _context.Items.Update(item);
                await _context.SaveChangesAsync();
            }
          
        }



        #endregion
        #region Delete item
        public async Task<int> DeleteItemByID(int? id)
        {
            // declare result
            int result = 0;
            if (_context != null)
            {
                var item = await _context.Items.FirstOrDefaultAsync(u => u.ItemId == id);
                if (item != null)
                {
                    // perform delete
                    _context.Items.Remove(item);
                    result = await _context.SaveChangesAsync(); // commit 
                    //return succcess;
                    result = 1;

                }
                return result;
            }
            return result;

           
        }

   
        #endregion
    }
}
