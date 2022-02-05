using Expense_Tracking.Models;
using Expense_Tracking.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracking.Repository
{
   public interface IExpense
    {
        Task<List<ExpenseView>> GetExpenses();

        Task<ExpenseView> GetExpenseByID(int? id);

        Task<ExpenseView> GetExpenseByPhone(Int64 id);

        Task<ExpenseView> GetExpenseByUsername(string name);

        Task<int> AddExpense(Expenses expense);

        Task UpdateExpense(Expenses expense);

        Task<int> DeleteExpenseByID(int? id);

        Task<List<ExpenseReport>> GetExpenseReport();

        Task<List<ListView>> GetExpensebyCost();
    }
}
