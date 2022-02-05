using Expense_Tracking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracking.Repository
{
    public class CategoryClass:ICategory
    {
        private readonly ExpenseTrackingContext _contextone;
        public CategoryClass(ExpenseTrackingContext contextone)
        {
            _contextone = contextone;
        }
        public async Task<int> AddCategory(Category category)
        {
            if (_contextone != null)
            {
                await _contextone.Category.AddAsync(category);
                await _contextone.SaveChangesAsync();
                return category.CatId;
            }
            return 0;
        }
       
        public async Task<int> DeleteCategoryById(int? id)
        {

            if (_contextone != null)
            {
                var category = await _contextone.Category.FirstOrDefaultAsync(cat => cat.CatId== id);
                if (category != null)
                {
                    _contextone.Category.Remove(category);

                    return await _contextone.SaveChangesAsync();
                }
                return 0;

            }
            return 0;

        }



        [HttpGet]
        public async Task<List<Category>> GetAllCategories()
        {
            if (_contextone != null)
            {
                var category = await _contextone.Category.ToListAsync();
                return category;

            }
            return null;
        }

        public async Task<Category> GetCategorybyId(int id)
        {
            if (_contextone != null)
            {
                var category = await _contextone.Category.FindAsync(id);
                return  category;
            }
            return null;
        }

        public async Task UpdateCategory(Category category)
        {
            if (_contextone != null)
            {
                _contextone.Entry(category).State = EntityState.Modified;
                _contextone.Category.Update(category);
                 await _contextone.SaveChangesAsync();
                
                
            }
           
          
        }
    }
}
