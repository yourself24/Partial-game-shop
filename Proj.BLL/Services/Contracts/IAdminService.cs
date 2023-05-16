using Proj.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.BLL.Services.Contracts
{
    public interface IAdminService
    {
        Task<List<Admin>> ReadAdmins();
        Task CreateAdmin (Admin admin);
        Task<Admin> ReadOneAdmin(int id);

        Admin ReadUserbyMail(string username);

        Task DeleteAdmin(int id);

        Task UpdateAdmin(int id, string username, string password);

        bool LoginAdmin(string username, string password);

    }
}
