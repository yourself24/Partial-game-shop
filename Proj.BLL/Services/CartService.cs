using Proj.BLL.Services.Contracts;
using Proj.DAL.Models;
using Proj.DAL.Repos.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly IGenericRepo<Cart> _cartRepo;

        public CartService(IGenericRepo<Cart> cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public async Task AddCart(Cart cart)
        {
            try
            {
                await _cartRepo.CreateItem(cart);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCart(int id)
        {
            try
            {
                await _cartRepo.DeleteItem(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Cart> ReadCart(int id)
        {
            try
            {
                return await _cartRepo.GetById(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Cart>> ReadCarts()
        {
            try
            {
                return await _cartRepo.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateCart(int id, int userId, decimal price)
        {
            var cart = await _cartRepo.GetById(id);
            if(cart == null)
            {
                throw new ArgumentException("No cart with this Id was found!");
            }
            if (id!= null && userId!= null && price != null)
            {
                cart.Id = id;
                cart.Price = price;
                cart.UserId = userId;

                await _cartRepo.UpdateItem(cart);
            }
        }

   
    }
}
