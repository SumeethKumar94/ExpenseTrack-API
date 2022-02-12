using Expense_Tracking.Models;
using Expense_Tracking.Repository;
using Expense_Tracking.View_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense_Tracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsers _users;
        private readonly IConfiguration _config;
        public UsersController(IUsers users,IConfiguration configuration)
        {
            _users = users;
            _config = configuration;
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
        
        [HttpGet]
        [Route("Login")]
        public async Task<ActionResult> GetUserByIdPass(string username, string password)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            //signing credential
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            //generate token
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);
            var response = Ok(new { token = ' ', empName = ' ', empPassword = ' ' });


            if (ModelState.IsValid)
            {
                try
                {
                    var tokens = new JwtSecurityTokenHandler().WriteToken(token);

                    try
                    {
                        if (_users != null)
                        {

                            var emp = await _users.GetUserByIdPass(username, password);
                            if (emp != null)
                            {
                                response = Ok(new { token = tokens, empName = emp.Name, empPassword = emp.Password, empId = emp.UserId });
                                return response;
                            }
                            else
                            {
                                return response = Ok(new { token = ' ', empName = "null", empPassword = ' ' });
                            }
                         }
                        else
                        {
                            return response = Ok(new { token = ' ', empName = ' ', empPassword = ' ' });
                        }
                    }
                    catch (NullReferenceException)
                    {
                        return response = Ok(new { token = ' ', empName = ' ', empPassword = ' ' });
                    }
                }
                catch (NullReferenceException)
                {
                    return response = Ok(new { token = ' ', employee = ' ' });
                }
            }
            return response;

           }
            [HttpPost]
        public async Task<Users> AddUser(Users user)
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
