using Proj.BLL.Services.Contracts;
using Proj.DAL.Models;
using Proj.DAL.Repos.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.BLL.Services
{


  
        public class UserPaymentService : IUserPaymentService
        {
            private readonly IGenericRepo<UserPayment> _userPaymentRepo;

            public UserPaymentService(IGenericRepo<UserPayment> userPaymentRepo)
            {
                _userPaymentRepo = userPaymentRepo;
            }

            public async Task AddUserPayment(UserPayment userPayment)
            {
                try
                {
                    await _userPaymentRepo.CreateItem(userPayment);
                }
                catch
                {
                    throw;
                }
            }

            public async Task DeleteUserPayment(int id)
            {
                try
                {
                    await _userPaymentRepo.DeleteItem(id);
                }
                catch
                {
                    throw;
                }
            }

            public Task<List<UserPayment>> GetAllUserPayments()
            {
                throw new NotImplementedException();
            }

            public async Task<UserPayment> GetUserPaymentById(int id)
            {
                try
                {
                    return await _userPaymentRepo.GetById(id);
                }
                catch
                {
                    throw;
                }
            }

            public async Task<List<UserPayment>> GetUserPayments()
            {
                try
                {
                    return await _userPaymentRepo.GetAll();
                }
                catch
                {
                    throw;
                }
            }

            public Task UpdateUserPayment(int id, int userId, int methodId, int cardNo, int cvv)
            {
                throw new NotImplementedException();
            }
        }
    }


