using Expense_Tracking.Models;
using Expense_Tracking.View_Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracking.Repository
{
    public interface IUsers
    {
        //get all users
        Task<List<UsersView>> GetUsers();

        //get user by id
        Task<UsersView> GetUserByID(int? id);
        Task<UsersView> GetUserByIdPass(string user,string pass);

        //add a user
        Task<Users> AddUser(Users user);

        //update a user
        Task UpdateUser(Users user);

        //delete a user
        Task<int> DeleteUserByID(int? id);
    }
}
