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
    public class DevService : IDeveloperService
    {
        private readonly IGenericRepo<Developer> _devRepo;
        public DevService(IGenericRepo<Developer> devRepo)
        {
            _devRepo = devRepo;
        }

        public async Task AddDev(Developer dev)
        {
            try
            {
                await _devRepo.CreateItem(dev);

            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteDev(int id)
        {
            try
            {
                await _devRepo.DeleteItem(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Developer>> GetAllDevs()
        {
            try
            {
                return await _devRepo.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Developer> GetDev(int id)
        {
            try
            {
                return await _devRepo.GetById(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateDev(Developer dev)
        {
            await _devRepo.UpdateItem(dev);
        }
        public async Task Update2(int id,string name,DateOnly founded)
        {
            var dev = await _devRepo.GetById(id);
            if (dev == null)
            {
                throw new ArgumentException("No developer with this id was found!");
            }
            if (id != null && name != null && founded != null)
            {
                dev.Id = id;
                dev.Name = name;
                dev.Founded = founded;
            }
            await _devRepo.UpdateItem(dev);
        }
    }
}
        
    
