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
    public class PlatformService : IGenericService<Platform>
    {
        private readonly IGenericRepo<Platform> _platformRepo;
        public PlatformService(IGenericRepo<Platform> platformRepo)
        {
            _platformRepo = platformRepo;
        }
        public async Task Create(Platform model)
        {
            try
            {
                await _platformRepo.CreateItem(model);
            }
            catch
            {
                throw;
            }
            
        }

        public async Task Delete(int id)
        {
            await _platformRepo.DeleteItem(id);
        }

        public async Task<List<Platform>> ReadAll()
        {
            try
            {
                return await _platformRepo.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Platform> ReadOne(int id)
        {
            try
            {
                return await _platformRepo.GetById(id);
            }
            catch
            {
                throw;
            }
        }

        public Task Update(Platform model)
        {
            throw new NotImplementedException();
        }

        public async Task Update2(int id,string name)
        {
            var platform = await _platformRepo.GetById(id);
            if (platform == null)
            {
                throw new ArgumentException("No platform with this id was found!");
            }
            if (id != null && name != null)
            {
                platform.Id = id;
                platform.Name = name;
            }
            await _platformRepo.UpdateItem(platform);
        }
    }
}
