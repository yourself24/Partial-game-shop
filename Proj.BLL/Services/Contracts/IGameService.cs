using Proj.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.BLL.Services.Contracts
{
    public interface IGameService
    {
        Task AddGame(Game game);
        Task DeleteGame(int id);
        Task<Game> ReadOneGame(int id);
        Task<List<Game>> ReadGames();
        Task UpdateGame(int id, string name, int? genreId, int? developerId, int? platformId,decimal? price, DateOnly releaseDate, int? stock);
    }
}
