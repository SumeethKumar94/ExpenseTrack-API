using Expense_Tracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracking.Repository
{
    public interface ICategory
    {
        Task<List<Category>> GetAllCategories();
        // Add Users
        Task<int> AddCategory(Category category);
        // Update user
        Task UpdateCategory(Category category);

        //Task GetUser

        Task<int> DeleteUserById(int? id);
        //  USING VIEW MODEL

        Task<Category> GetCategorybyId(int id);
    }
}
