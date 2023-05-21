using Proj.BLL.Services.Contracts;
using Proj.DAL.Models;
using Proj.DAL.Repos.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;

namespace Proj.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepo<User> _userRepo;
        private readonly IAccountCreationObservable _accountCreationObservable;
        public UserService(IGenericRepo<User> userRepo, IAccountCreationObservable accountCreationObservable)
        {
            _userRepo = userRepo;
            _accountCreationObservable = accountCreationObservable;
            _accountCreationObservable.RegisterObserver(new LogAccountCreationObserver());
            _accountCreationObservable.RegisterObserver(new EmailAccountCreationObserver(
           smtpServer: "smtp.mailtrap.io",
           smtpPort: 2525   ,
           smtpUsername: "a844e3d7a523cc",
           smtpPassword: "98bf6bee0133b6",
           fromEmail: "lucabar52@gmail.com",
           toEmail: "lucabar24@gmail.com"
       ));
        }

        public UserService()
        {
        }

        public async Task DeleteUser(int id)
        {
            try
            {
                await _userRepo.DeleteItem(id);
            }
            catch
            {
                throw;
            }
        }

        public bool LoginUser(string username, string password)
        {
            var user = _userRepo.FindUser(username);
            if (user == null)
            {
                return false;
            }
            else
            {
                if (username==user.Email && BCrypt.Net.BCrypt.Verify(password.ToLower(),user.Password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<User> ReadOneUser(int id)
        {
            try
            {
                return await _userRepo.GetById(id);
            }
            catch
            {
                throw;
            }
        }
        public async Task CreateUser(User user)
        {
            try
            {
                await _userRepo.CreateItem(user);
                _accountCreationObservable.NotifyObservers(user);
            }
            catch
            {
                throw;
            }
        }

        public User ReadUserbyMail(string username)
        {
            return _userRepo.FindUser(username);
        }

        public async Task<List<User>> ReadUsers()
        {
            try
            {
                return await _userRepo.GetAll();
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<User> GetUsersWithShorterUsernames(int length)
        {
            var users = _userRepo.GetAll().Result; // wait for the asynchronous GetAll() method to complete
            foreach (var user in users)
            {
                if (user.Username.Length < length)
                {
                    yield return user; // return the user using the iterator
                }
            }
        }
        public async Task<UserIterator> ReadUsers2()
        {
            try
            {
                var users = await _userRepo.GetAll();
                return new UserIterator(users);
            }
            catch
            {
                throw;
            }
        }



        public async Task UpdateUser(int id, string username, string password, string email, string address,int purchaes)
        {
          
                var user = await _userRepo.GetById(id);
                if (user == null)
                {
                    throw new ArgumentException("No user with this id was found!");
                }
                if (id != null && username != null && password != null && email != null && address != null && purchaes!=null)
                {
                    user.Id = id;
                    user.Username = username;
                user.Password = BCrypt.Net.BCrypt.HashPassword(password);
                user.Email = email;
                user.Address = address;
                user.Purchases = purchaes;
                }
                await _userRepo.UpdateItem(user);

            
        }

    }
}
