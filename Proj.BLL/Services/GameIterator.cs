using Proj.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.BLL.Services
{

    public class GameIterator : IIterator<Game>
    {
        private List<Game> _games;
        private int _position;
        public GameIterator(List<Game> games)
        {
            _games = games;
            _position = 0;
        }

        public Task<bool> HasNextAsync()
        {
            return Task.FromResult(_position < _games.Count);
        }

        public Task<Game> NextAsync()
        {
            if (HasNextAsync().Result)
            {
                Game game = _games[_position];
                _position++;
                return Task.FromResult(game);
            }
            else
            {
                throw new InvalidOperationException("No more elements in the collection.");
            }
        }
    }
}
