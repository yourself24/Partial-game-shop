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
    public class GenreService : IGenericService<Genre>
    {
        private readonly IGenericRepo<Genre> _genreService;
        public GenreService(IGenericRepo<Genre> genreService)
        {
            _genreService = genreService;
        }
        public async Task Create(Genre model)
        {
            try
            {
                await _genreService.CreateItem(model);
            }
            catch
            {
                throw;
            }
            
        }

        public async Task Delete(int id)
        {
            try
            {
                await _genreService.DeleteItem(id);

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Genre>> ReadAll()
        {
            return await _genreService.GetAll();
        }

        public async Task<Genre> ReadOne(int id)
        {
            return await _genreService.GetById(id);
        }

        public Task Update(Genre model)
        {
            throw new NotImplementedException();
        }
        public async Task Update2(int id,string name)
        {
            var genre = await _genreService.GetById(id);
            if(genre== null)
            {
                throw new ArgumentException("No genre with this id was found!");
            }
            if(id!= null && name!= null)
            {
                genre.Id = id;
                genre.Name = name;
            }
            await _genreService.UpdateItem(genre);

        }
    }
}
