using Proj.DAL.Models;
using Proj.DAL.Repos.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proj.BLL.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly IGenericRepo<CartItem> _cartItemRepo;

        public CartItemService(IGenericRepo<CartItem> cartItemRepo)
        {
            _cartItemRepo = cartItemRepo;
        }

        public async Task AddCartItem(CartItem cartItem)
        {
            try
            {
                await _cartItemRepo.CreateItem(cartItem);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCartItem(int id)
        {
            try
            {
                await _cartItemRepo.DeleteItem(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CartItem>> ReadCartItems()
        {
            try
            {
                return await _cartItemRepo.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public async Task<CartItem> ReadOneCartItem(int id)
        {
            try
            {
                return await _cartItemRepo.GetById(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateCartItem(CartItem cartItem)
        {
            try
            {
                await _cartItemRepo.UpdateItem(cartItem);
            }
            catch
            {
                throw;
            }
        }

        public async Task Update2(int id,int gameId,int cartId,int quantity,decimal price)
        {
            var item = await _cartItemRepo.GetById(id);
            if(item == null)
            {
                throw new ArgumentException("No item with this ID was found!");
            }
            if(id !=null && gameId !=null && cartId!=null && quantity!=null && price != null)
            {
                item.Id = id;
                item.Game= gameId;
                item.Cart = cartId;
                item.Quantity = quantity;
                item.Price = price;

                await _cartItemRepo.UpdateItem(item);
            }
        }

        public async Task<ItemIterator> ReadItems2()
        {
            var items =  await _cartItemRepo.GetAll();
            return new ItemIterator(items);
        }
    }
}
