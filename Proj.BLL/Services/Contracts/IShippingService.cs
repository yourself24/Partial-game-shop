using Proj.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.BLL.Services.Contracts
{
    public interface IShippingService
    {
        Task<List<Shipping>> ReadAll();
        Task<Shipping> ReadOne(int id);

        Task Create(Shipping model);

        Task Update(Shipping model);

        Task Delete(int id);
    }
}
