using IPWService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPWService.IRepository
{
    public interface IUsersRepository
    {
        Task<List<Users>> GetUsers();

        Task<Users> GetUser(int? UserId);

        Task<int> AddUser(Users User);

        Task<int> DeleteUser(int? UserId);

        Task UpdateUser(Users User);
    }
}
