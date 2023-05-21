using Proj.BLL.Services.Contracts;
using Proj.DAL.Models;
using Proj.DAL.Repos.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proj.BLL.Services
{
    public class ShippingService : IShippingService
    {
        private readonly IGenericRepo<Shipping> _shipRepo;

        public ShippingService(IGenericRepo<Shipping> shipRepo)
        {
            _shipRepo = shipRepo;
        }

        public async Task Create(Shipping model)
        {
            try
            {
                await _shipRepo.CreateItem(model);
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(Shipping model)
        {
            try
            {
                await _shipRepo.UpdateItem(model);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Shipping>> ReadAll()
        {
            return await _shipRepo.GetAll();
        }

        public async Task<Shipping> ReadOne(int id)
        {
            return await _shipRepo.GetById(id);
        }

        public async Task Delete(int id)
        {
            try
            {
                await _shipRepo.DeleteItem(id);
            }
            catch
            {
                throw;
            }
        }
        public async Task Update2(int id, string name, decimal price, int delivery_time)
        {
            var ship = await _shipRepo.GetById(id);
            if (ship == null)
            {

                throw new ArgumentException("No Shipment with this id was found!");
            }
            if (id != null && name != null && price != null & delivery_time != null)
            {
                ship.Name = name;
                ship.Id = id;
                ship.Price = price;
                ship.DeliveryTime = delivery_time;
            }
            await _shipRepo.UpdateItem(ship);
        }
    }
}
