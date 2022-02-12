using Expense_Tracking.Models;
using Expense_Tracking.Repository;
using Expense_Tracking.View_Model;
using Microsoft.AspNetCore.Authorization;
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
    public class ExpensesController : ControllerBase
    {
        private readonly IExpense _expense;
        private readonly IConfiguration _config;

        public ExpensesController(IExpense expenses,IConfiguration config)
        {
            _expense = expenses;
            _config = config;
        }

        #region Get all Expense
        [HttpGet]
       //[Authorize]
        public async Task<List<ExpenseView>> GetExpenses()
        {
            return await _expense.GetExpenses();
        }
        #endregion
        [HttpGet]
        [Route("GetToken")]
        public IActionResult GenerateJWToken()
        {
            //security key
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            //signing credential
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            //generate token
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials);

            var tokens =new JwtSecurityTokenHandler().WriteToken(token);
            var response = Ok(new { token = tokens });
            return response;

        }

        #region Get  Expense by Phone
        [HttpGet]
        [Route("Phone/{phone}")]
        //https://localhost:44382/api/Expenses/Phone/9870987654
        public async Task<ExpenseView> GetExpenseByPhone(Int64 phone)
        {
            return await _expense.GetExpenseByPhone(phone);
        }
        #endregion

        #region Get  Expense by Username
        [HttpGet]
        [Route("Username")]
        //https://localhost:44382/api/Expenses/Username/?name=Steve12
        public async Task<List<ExpenseView>> GetExpenseByUsername(string name)
        {
            return await _expense.GetExpenseByUsername(name);
        }
        #endregion

        #region Get  Expense by Id
        [HttpGet]
        [Route("Id/{id}")]
        //https://localhost:44382/api/Expenses/Id/1
        public async Task<ExpenseView> GetExpenseByID(int id)
        {
            return await _expense.GetExpenseByID(id);
        }
        #endregion


        #region Get  Expense by Week
        [HttpGet]
        [Route("GetWeekReport")]
        //https://localhost:44382/api/Expenses/GetWeekReport
        public async Task<List<ExpenseReport>> GetExpenseReport()
        {
            return await _expense.GetExpenseReport();
        }
        #endregion

        #region Get List of Item increasing order of Cost
        [HttpGet]
        [Route("GetExpenseCost")]
        public async Task<List<ListView>> GetExpensebyCost()
        {
            return await _expense.GetExpensebyCost();
        }
            #endregion
            #region Add Expense
            [HttpPost]
        public async Task <int> AddExpense(Expenses expense)
        {
            return await _expense.AddExpense(expense);
        }
        #endregion
        #region Update  Expense
        [HttpPut]
        public async Task<IActionResult> UpdateExpense(Expenses expense)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _expense.UpdateExpense(expense);
                    return Ok(expense);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        #endregion
    }

}
