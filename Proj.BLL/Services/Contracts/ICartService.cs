using Proj.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.BLL.Services.Contracts
{
    public interface ICartService
    {
        Task AddCart(Cart cart);
        Task DeleteCart(int id);
        Task<Cart> ReadCart(int id);

        Task<List<Cart>> ReadCarts();
        Task UpdateCart(int id, int userId, decimal price);
    }
}
