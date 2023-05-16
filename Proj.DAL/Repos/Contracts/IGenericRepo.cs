using Proj.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.DAL.Repos.Contracts
{
    public interface IGenericRepo<TModel> where TModel : class
    {
        Task<List<TModel>> GetAll();
        Task<TModel> GetById(int id);

        //Task<List<TModel>> GetAllWithId(int id);


        // Tasks for Genre
        Task DeleteItem(int id);

        Task CreateItem(TModel model);

        Task UpdateItem(TModel model);

        User FindUser(string name);

        Admin FindByString(string name);


    }
}
