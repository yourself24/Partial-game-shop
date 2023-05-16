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
   
    namespace Proj.BLL.Services
    {
        public class GameService : IGameService
        {
            private readonly IGenericRepo<Game> _gameRepo;

            public GameService(IGenericRepo<Game> gameRepo)
            {
                _gameRepo = gameRepo;
            }

            public async Task AddGame(Game game)
            {
                try
                {
                    await _gameRepo.CreateItem(game);
                }catch
                { 
                    throw;
                }
            }

            public async Task DeleteGame(int id)
            {
                try
                {
                    await _gameRepo.DeleteItem(id);
                }
                catch
                {
                    throw;
                }
            }

            public async Task<Game> ReadOneGame(int id)
            {
                try
                {
                    return await _gameRepo.GetById(id);
                }
                catch
                {
                    throw;
                }
            }

            public async Task<List<Game>> ReadGames()
            {
                try
                {
                    return await _gameRepo.GetAll();
                }
                catch
                {
                    throw;
                }
            }

            public async Task UpdateGame(int id, string name, int? genreId, int? developerId, int? platformId, decimal? price, DateOnly releaseDate, int? stock)
            {
                var game = await _gameRepo.GetById(id);
                if (game == null)
                {
                    throw new ArgumentException("No game with this id was found!");
                }
                if (name != null && genreId != null && developerId != null && platformId != null && price != null && releaseDate != null && stock != null)
                {
                    game.Name = name;
                    game.Genre = genreId;
                    game.Developer = developerId;
                    game.Platform = platformId;
                    game.Price = price;
                    game.ReleaseDate = releaseDate;
                    game.Stock = stock;
                }
                await _gameRepo.UpdateItem(game);
            }
        }
    }

}
