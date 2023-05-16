using Microsoft.CodeAnalysis.CSharp.Syntax;
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
    public class BillService : IGenericService<Bill>
    {
        private readonly IGenericRepo<Bill> _billRepo;
        public BillService(IGenericRepo<Bill> repo)
        {
            _billRepo = repo;
        }
        public async Task Create(Bill model)
        {
            try
            {
                await _billRepo.CreateItem(model);
            }
            catch {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await _billRepo.DeleteItem(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Bill>> ReadAll()
        {
            try
            {
                return await _billRepo.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Bill> ReadOne(int id)
        {
            try
            {

                return await _billRepo.GetById(id);

            }
            catch
            {
                throw;
            }
        }

        public Task Update(Bill model)
        {
            throw new NotImplementedException();
        }
        public async Task Update2(int  id,int cartId,int shipmentId,int paymentID,decimal price)
        {
            var bill = await _billRepo.GetById(id);
            if (bill == null)
            {
                throw new ArgumentException("No Bill Found");
            }
            if(id !=null &&cartId!=null && shipmentId!=null && paymentID!=null && price != null)
            {
                bill.Id= id;
                bill.Cart = cartId;
                bill.Shipment = shipmentId;
                bill.PaymentMethod = paymentID;
                bill.Price = price;

                await _billRepo.UpdateItem(bill);
            }
            else
            {
                throw new ArgumentException("Invalid Data!");
            }

        }
    }
}
