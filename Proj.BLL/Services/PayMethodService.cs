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
    public class PayMethodService : IGenericService<PayMethod>
    {
        private readonly IGenericRepo<PayMethod> _paymentRepo;
        public PayMethodService(IGenericRepo<PayMethod> paymentRepo)
        {
            _paymentRepo = paymentRepo;
        }
        public async Task Create(PayMethod model)
        {
            try
            {
                await _paymentRepo.CreateItem(model);
            }
            catch
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await _paymentRepo.DeleteItem(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<PayMethod>> ReadAll()
        {
            return await _paymentRepo.GetAll();
        }

        public async Task<PayMethod> ReadOne(int id)
        {
            return await _paymentRepo.GetById(id);
        }

        public async Task Update(PayMethod model)
        {
            try
            {
                await _paymentRepo.UpdateItem(model);
            }
            catch
            {
                throw;
            }
        }
        public async Task Update2(int id, string name)
        {
            var pay = await _paymentRepo.GetById(id);
            if (pay == null)
            {
                throw new ArgumentException("No payment method with this id was found!");
            }
            if (id != null && name != null)
            {
                pay.Id = id;
                pay.Name = name;
            }
            await _paymentRepo.UpdateItem(pay);

        }
    }
    
}
