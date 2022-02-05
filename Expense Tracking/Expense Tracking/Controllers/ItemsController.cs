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
    public class ItemsController : ControllerBase
    {
     
            private readonly IItems _items;
            public ItemsController(IItems itemRepo)
            {
                _items = itemRepo;
            }
            [HttpGet]
            public async Task<List<Items>> GetItems()
            {
                return await _items.GetItems();
            }

            //get item by id
            [HttpGet("{id}")]

            public async Task<ActionResult<Items>> GetItemByID(int? id)
            {
                //return await _userRepository.GetUserByID(id);
                try
                {
                    var itemOne = await _items.GetItemByID(id);
                    if (itemOne == null)
                    {
                        return NotFound();
                    }
                    return itemOne;
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }

            //add an item
            [HttpPost]
            public async Task<ActionResult<Items>> AddItem(Items item)
            {
                return await _items.AddItem(item);
            }

            //update an item

            #region update an item
            [HttpPut]
            public async Task<IActionResult> UpdateItem([FromBody] Items item)
            {
                //since it is frombody we need to check the validation of body
                if (ModelState.IsValid)
                {
                    try
                    {
                        await _items.UpdateItem(item);
                        return Ok(item);
                    }
                    catch (Exception)
                    {
                        return BadRequest();
                    }
                }
                return BadRequest();
            }
            #endregion

            //delete item
            #region delete item by id
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteItemByID(int? id)
            {
                int result = 0;
                if (id == null)
                {
                    return BadRequest();
                }
                try
                {
                    result = await _items.DeleteItemByID(id);
                    if (result == 0)
                    {
                        return NotFound();
                    }
                    return Ok("delete successfull");
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            #endregion
        }
    }

