using Expense_Tracking.Models;
using Expense_Tracking.View_Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracking.Repository
{
    public class ExpenseClass:IExpense
    {
        private readonly ExpenseTrackingContext _contextone;
        public ExpenseClass(ExpenseTrackingContext trackingContext)
        {
            _contextone = trackingContext;
        }

        public async Task<int> AddExpense(Expenses expense)
        {
            if (_contextone != null)
            {
                await _contextone.Expenses.AddAsync(expense);
                await _contextone.SaveChangesAsync();
                return expense.ExpId;
            }
            return 0;
        }

        public async Task<int> DeleteExpenseByID(int? id)
        {
            if (_contextone != null)
            {
                var expenses = await _contextone.Expenses.FirstOrDefaultAsync(exp => exp.ExpId == id);
                if (expenses != null)
                {
                    _contextone.Expenses.Remove(expenses);

                    return await _contextone.SaveChangesAsync();
                }
                return 0;

            }
            return 0;
        }

        public async Task<List<ListView>> GetExpensebyCost()
        {
            if (_contextone != null)
            {
                return await (
                              from l in _contextone.ItemList
                              join i in _contextone.Items
                              on l.ItemId equals i.ItemId
                              join c in _contextone.Category
                              on i.CatId equals c.CatId
                              orderby i.ItemPrice
                              select new ListView
                              {
                                  ItemName=i.ItemName,
                                  ExpenseType=c.CatName,
                                  ItemCost=i.ItemPrice

                              }).ToListAsync();
            }
            return null;
        }

        public async Task<ExpenseView> GetExpenseByID(int? id)
        {
            if (_contextone != null)
            {
                return await(from e in _contextone.Expenses
                             join u in _contextone.Users
                             on e.Userid equals u.Userid
                             where u.Userid == id
                             select new ExpenseView
                             {
                                 ExpId = e.ExpId,
                                 Userid = u.Userid,
                                 Username = u.Name,
                                 Phone = u.Phoneno,
                                 ExpenseDate = e.ExpenseDate,
                                 Itemlist = (from l in _contextone.ItemList
                                             join i in _contextone.Items
                                              on l.ItemId equals i.ItemId
                                             join c in _contextone.Category
                                             on i.CatId equals c.CatId
                                             where l.ExpId == e.ExpId
                                             select new ItemView
                                             {
                                                 ItemId = i.ItemId,
                                                 ItemName = i.ItemName,
                                                 ItemPrice = i.ItemPrice,
                                                 Category = c.CatName
                                             }).ToList(),
                                 TotalExp = e.TotalExp
                             }).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<ExpenseView> GetExpenseByPhone(Int64 id)
        {
            if (_contextone != null)
            {
                return await(from e in _contextone.Expenses
                             join u in _contextone.Users
                             on e.Userid equals u.Userid
                             where u.Phoneno == id
                             select new ExpenseView
                             {
                                 ExpId = e.ExpId,
                                 Userid = u.Userid,
                                 Username = u.Name,
                                 Phone = u.Phoneno,
                                 ExpenseDate = e.ExpenseDate,
                                 Itemlist = (from l in _contextone.ItemList
                                             join i in _contextone.Items
                                              on l.ItemId equals i.ItemId
                                             join c in _contextone.Category
                                             on i.CatId equals c.CatId
                                             where l.ExpId == e.ExpId
                                             select new ItemView
                                             {
                                                 ItemId = i.ItemId,
                                                 ItemName = i.ItemName,
                                                 ItemPrice = i.ItemPrice,
                                                 Category = c.CatName
                                             }).ToList(),
                                 TotalExp = e.TotalExp
                             }).FirstOrDefaultAsync();
            }
            return null;
        }
        [HttpGet("{name}")]
        public async Task<List<ExpenseView>> GetExpenseByUsername(string name)
        {
            if (_contextone != null)
            {
                return await (from e in _contextone.Expenses
                              join u in _contextone.Users
                              on e.Userid equals u.Userid
                              where u.Name == name
                              select new ExpenseView
                              {
                                  ExpId = e.ExpId,
                                  Userid = u.Userid,
                                  Username = u.Name,
                                  Phone = u.Phoneno,
                                  ExpenseDate = e.ExpenseDate,
                                  Itemlist = (from l in _contextone.ItemList
                                              join i in _contextone.Items
                                               on l.ItemId equals i.ItemId
                                              join c in _contextone.Category
                                              on i.CatId equals c.CatId
                                              where l.ExpId == e.ExpId
                                              select new ItemView
                                              {
                                                  ItemId = i.ItemId,
                                                  ItemName = i.ItemName,
                                                  ItemPrice = i.ItemPrice,
                                                  Category = c.CatName
                                              }).ToList(),
                                  TotalExp = e.TotalExp
                              }).ToListAsync();
            }
            return null;
        }

        public async Task<List<ExpenseReport>> GetExpenseReport()
        {
            if (_contextone != null)
            {

                return await (from e in _contextone.Expenses
                              join u in _contextone.Users
                              on e.Userid equals u.Userid
                              join l in _contextone.ItemList
                              on e.ItemlId equals l.ItemlId
                              join i in _contextone.Items
                              on l.ItemId equals i.ItemId
                            orderby e.ExpenseDate
                              select new ExpenseReport
                              {
                                  Name =u.Name,
                                  ItemName=i.ItemName,
                                 Week = (int)Math.Truncate((double)(e.ExpenseDate.Value.DayOfYear / 7)) + 1,
                                // For Month  Month=e.ExpenseDate.Value.ToString("MMMM"),
                                TotalExpense = e.TotalExp
                              }).ToListAsync();
            }
            return null;
        }


        public async Task<List<ExpenseView>> GetExpenses()
        {
            if (_contextone != null)
            {
                return await (from e in _contextone.Expenses
                              join u in _contextone.Users
                              on e.Userid equals u.Userid
                              select new ExpenseView
                              {
                                  ExpId = e.ExpId,
                                  Userid = u.Userid,
                                  Username = u.Name,
                                  Phone = u.Phoneno,
                                  ExpenseDate = e.ExpenseDate,
                                  Itemlist = (from l in _contextone.ItemList
                                              join i in _contextone.Items
                                               on l.ItemId equals i.ItemId
                                              join c in _contextone.Category
                                              on i.CatId equals c.CatId
                                              where l.ExpId == e.ExpId
                                              select new ItemView
                                              {
                                                  ItemId = i.ItemId,
                                                  ItemName = i.ItemName,
                                                  ItemPrice = i.ItemPrice,
                                                  Category = c.CatName
                                              }).ToList(),
                                  TotalExp = e.TotalExp
                              }).ToListAsync();
            }
            return null;
            }

        public async Task UpdateExpense(Expenses expense)
        {
            if (_contextone != null)
            {
                _contextone.Entry(expense).State = EntityState.Modified;
                _contextone.Expenses.Update(expense);
                await _contextone.SaveChangesAsync();
            }
        }
    }
}
