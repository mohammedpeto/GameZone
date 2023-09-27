using GameZone.Models;
using GameZone.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameZone.Services
{
    public interface IGameRepo
    {
        public List<Game> GetAll();
        public Game GetByID(int id);
        public Task Create(CreateGameViewModel game);
        public void Update(Game game, int id);
        public void Delete(int id);
    }
}
