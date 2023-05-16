using Proj.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.BLL.Services
{
    public class UserIterator : IIterator<User>
    {
        private List<User> _users;
        private int _position;

        public UserIterator(List<User> users)
        {
            _users = users;
            _position = 0;
        }

        public Task<bool> HasNextAsync()
        {
            return Task.FromResult(_position < _users.Count);
        }


        public Task<User> NextAsync()
        {
            if (HasNextAsync().Result)
            {
                User user = _users[_position];
                _position++;
                return Task.FromResult(user);
            }
            else
            {
                throw new InvalidOperationException("No more elements in the collection.");
            }
        }
    }

}
