using Proj.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.BLL.Services.Contracts
{
    public interface IUserPaymentService
    {
        Task AddUserPayment(UserPayment userPayment);
        Task DeleteUserPayment(int id);

        Task<UserPayment> GetUserPaymentById(int id);
        Task<List<UserPayment>> GetAllUserPayments();
        Task UpdateUserPayment(int id, int userId, int methodId, int cardNo, int cvv);
    }
}
