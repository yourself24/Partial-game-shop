using Proj.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proj.BLL.Services
{
    public interface ICartItemService
    {
        Task AddCartItem(CartItem item);
        Task DeleteCartItem(int id);
        Task<List<CartItem>> ReadCartItems();
        Task<CartItem> ReadOneCartItem(int id);
        Task UpdateCartItem(CartItem item);
      
    }
}
