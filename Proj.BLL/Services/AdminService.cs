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
    public class AdminService : IAdminService
    {
        private readonly IGenericRepo<Admin> _adminRepo;

        public AdminService(IGenericRepo<Admin> adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public async Task DeleteAdmin(int id)
        {
            try
            {
                await _adminRepo.DeleteItem(id);
            }
            catch
            {
                throw;
            }
        }

        public bool LoginAdmin(string username, string password)
        {
            var admin = _adminRepo.FindByString(username);
            if (admin == null)
            {
                return false;
            }
            else
            {
                if (BCrypt.Net.BCrypt.Verify(password, admin.Password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<Admin> ReadOneAdmin(int id)
        {
            try
            {
                return await _adminRepo.GetById(id);
            }
            catch
            {
                throw;
            }
        }

        public Admin ReadUserbyMail(string email)
        {
            return _adminRepo.FindByString(email);
        }

        public async Task<List<Admin>> ReadAdmins()
        {
            try
            {
                return await _adminRepo.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateAdmin(int id,string username, string password)
        {
            var admin = await _adminRepo.GetById(id);
            if (admin == null)
            {
                throw new ArgumentException("No admin with this id was found!");
            }
            if(id!= null && username !=null && password != null)
            {
                admin.Id = id;
                admin.Email = username;
                admin.Password = BCrypt.Net.BCrypt.HashPassword(password);
            }
            await _adminRepo.UpdateItem(admin);


        }

        public async Task CreateAdmin(Admin admin)
        {
            try
            {
                await _adminRepo.CreateItem(admin);
            }
            catch
            {
                throw;
            }
        }
    }           
        
    }

