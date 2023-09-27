using GameZone.Data;
using GameZone.Models;
using GameZone.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameZone.Services
{
    public class GameRepo:IGameRepo
    {
        public AppDbContext context { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }
        private readonly string _ImagesPath;

        public GameRepo(AppDbContext _context, IWebHostEnvironment _webHostEnvironment)
        {
            context = _context;
            WebHostEnvironment = _webHostEnvironment;
            _ImagesPath = $"{_webHostEnvironment.WebRootPath}/assests/images/games";
        }

        public List<Game> GetAll()
        {
            return context.Games
                .Include(d=>d.Category)
                .Include(d=>d.GameDevices)
                .ThenInclude(g=>g.Device)
                .ToList();
        }

        public Game GetByID(int id)
        {
            return context.Games
                .Include(d => d.Category)
                .Include(d => d.GameDevices)
                .ThenInclude(g => g.Device)
                .FirstOrDefault(g => g.Id == id);
        }

        public async Task Create(CreateGameViewModel game)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(game.Cover.FileName)}";
            //save cover photo on server
            var path = Path.Combine(_ImagesPath,coverName);
            using var stream = File.Create(path);
            await game.Cover.CopyToAsync(stream);
                

            Game game1 = new()
            {
                Name = game.Name,
                Description = game.Description,
                CategoryID = game.CategoryID,
                Cover = coverName,
                GameDevices = game.SelectedDevices.Select(d => new GameDevice { DeviceID = d }).ToList()
            };
            context.Games.Add(game1);
            context.SaveChanges();
        }

        public void Update(Game game, int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
