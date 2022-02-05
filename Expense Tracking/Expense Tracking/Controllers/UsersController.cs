using Expense_Tracking.Models;
using Expense_Tracking.Repository;
using Expense_Tracking.View_Model;
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
    public class UsersController : ControllerBase
    {
        private readonly IUsers _users;
        public UsersController(IUsers users)
        {
            _users = users;
        }
        [HttpGet]
        public async Task<List<UsersView>> GetUsers()
        {
            return await _users.GetUsers();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<UsersView>> GetUserByID(int? id)
        {
            //return await _users.GetUserByID(id);
            try
            {
                var userOne = await _users.GetUserByID(id);
                if (userOne == null)
                {
                    return NotFound();
                }
                return userOne;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Users>> AddUser(Users user)
        {
            return await _users.AddUser(user);
        }

        //update a user
        #region update a user
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] Users user)
        {
            //since it is frombody we need to check the validation of body
            if (ModelState.IsValid)
            {
                try
                {
                    await _users.UpdateUser(user);
                    return Ok(user);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        #endregion

        //delete user
        #region delete user by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserByID(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                result = await _users.DeleteUserByID(id);
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
