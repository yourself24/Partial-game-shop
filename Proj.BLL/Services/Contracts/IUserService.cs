using Proj.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.BLL.Services.Contracts
{
    public interface IUserService
    {
        Task<List<User>> ReadUsers();
        Task CreateUser(User user);
        Task<User> ReadOneUser(int id);

        User ReadUserbyMail(string username);

        Task DeleteUser(int id);

        Task UpdateUser(int id,string username,string password,string email,string address);

        bool LoginUser(string username, string password);
        IEnumerable<User> GetUsersWithShorterUsernames(int length);
         Task<UserIterator> ReadUsers2();

    }
}
