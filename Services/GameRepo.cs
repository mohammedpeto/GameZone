using GameZone.Data;
using GameZone.Models;
using GameZone.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            var coverName = await SaveCover(game.Cover);
                

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

        public async Task<Game?> Update(EditGameViewModel model)
        {
            var game = context.Games
                .Include(g => g.GameDevices)
                .SingleOrDefault(g => g.Id == model.Id);

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryID = model.CategoryID;
            game.GameDevices = model.SelectedDevices.Select(d => new GameDevice { DeviceID = d }).ToList();

            var hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;
            if(hasNewCover)
            {
                game.Cover = await SaveCover(model.Cover);
            }

           var rows = context.SaveChanges();
            // Save the new Photo on Server instead of the old one
            if(rows > 0)
            {
                if(hasNewCover)
                {
                    var co = Path.Combine(_ImagesPath, oldCover);
                    File.Delete(co);
                }
                return game;
            }
            else
            {
                var co = Path.Combine(_ImagesPath, game.Cover);
                File.Delete(co);
                return null;
            }
           
        }

        public bool Delete(int id)
        {
            var game = context.Games.Find(id);
            if (game == null)
                return false;

            context.Games.Remove(game);
            var rows = context.SaveChanges();
            if(rows > 0)
            {
                var cover = Path.Combine(_ImagesPath, game.Cover);
                File.Delete(cover);
            }
            return true;
        }

        private async Task<string> SaveCover (IFormFile Cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}";
            //save cover photo on server
            var path = Path.Combine(_ImagesPath, coverName);
            using var stream = File.Create(path);
            await Cover.CopyToAsync(stream);

            return coverName;
        }
    }
}
