using Expense_Tracking.Models;
using Expense_Tracking.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategory _category;

        //constructor injection
        public CategoriesController(ICategory category)
        {
            _category = category;
        }
        [HttpGet]
        [Route("GetCategories")]
        public async Task<List<Category>> GetAllCategories()
        {

            return await _category.GetAllCategories();
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<Category> GetCategorybyId(int id)
        {
            return await _category.GetCategorybyId(id);
        }
    }
}
