using Proj.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.BLL.Services
{
    public class ItemIterator : IIterator<CartItem>
    {
        private List<CartItem> _items;
        private int _position;

        public ItemIterator(List<CartItem> items)
        {
            _items = items;
            _position = 0;
        }

        public Task<bool> HasNextAsync()
        {
            return Task.FromResult(_position < _items.Count);
        }


        public Task<CartItem> NextAsync()
        {
            if (HasNextAsync().Result)
            {
                CartItem item = _items[_position];
                _position++;
                return Task.FromResult(item);
            }
            else
            {
                throw new InvalidOperationException("No more elements in the collection.");
            }
        }
    }

}
