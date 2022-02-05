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
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _category;
        public CategoryController(ICategory category)
        {
            _category = category;
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var catID = await _category.AddCategory(category);
                    if (catID > 0)
                    {
                        return Ok(catID);
                    }
                    return NotFound();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();

        }
        [HttpGet]
        public async Task<List<Category>> GetAllCategories()
        {
            return await _category.GetAllCategories();
        }
        [HttpGet("{id}")]
        public async Task<Category> GetCategorybyId(int id)
        {
            return await _category.GetCategorybyId(id);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _category.UpdateCategory(category);
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();

        }
        public async Task<IActionResult> DeleteCategoryById(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var employee = await _category.DeleteCategoryById(id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}
