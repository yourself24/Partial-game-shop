using Proj.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.BLL.Services.Contracts
{
    public interface IDeveloperService
    {
        Task<List<Developer>> GetAllDevs();

        Task AddDev(Developer dev);
        
        Task DeleteDev(int id);

        Task<Developer> GetDev(int id);
    
        Task UpdateDev(Developer dev);

        Developer GetDevByName(string name);
    }
}
