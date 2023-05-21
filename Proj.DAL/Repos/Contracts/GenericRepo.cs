using Microsoft.EntityFrameworkCore;
using Proj.DAL.DataContext;
using Proj.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.DAL.Repos.Contracts
{
    public class GenericRepo<TModel> : IGenericRepo<TModel> where TModel : class
    {
        private readonly VshopContext _context;
        private readonly DbSet<TModel> _dbSet;
        public GenericRepo(VshopContext context)
        {
            _context = context;
            _dbSet = context.Set<TModel>();
        }
        public async Task CreateItem(TModel model)
        {
            await _dbSet.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItem(int id)
        {
            var item = await _dbSet.FindAsync(id);
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TModel>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TModel> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }



        public async Task UpdateItem(TModel model)
        {
            _dbSet.Update(model);
            await _context.SaveChangesAsync();
        }
        public User FindUser(string name)
        {
            return  _context.Users.FirstOrDefault(user=>user.Email == name);
        }
        public Admin FindByString(string name)
        {
            return _context.Admins.FirstOrDefault(admin => admin.Email == name);
        }

        public Developer FindDeveloper(string name)
        {
            return _context.Developers.FirstOrDefault(dev => dev.Name == name);
        }
    }
}
