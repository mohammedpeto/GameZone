using GameZone.Data;
using GameZone.Services;
using GameZone.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameZone.Controllers
{
    public class GameController : Controller
    {
        public ICategoryRepo categoryRepo { get; }
        public IDevicesRepo DevicesRepo { get; }
        public IGameRepo GameRepo { get; }

        public GameController(ICategoryRepo _categoryRepo , IDevicesRepo _devicesRepo , IGameRepo _gameRepo)
        {
            categoryRepo = _categoryRepo;
            DevicesRepo = _devicesRepo;
            GameRepo = _gameRepo;
        }

        public IActionResult Index()
        {
            var games = GameRepo.GetAll();
            return View(games);
        }

        public IActionResult Details(int id)
        {
            var game = GameRepo.GetByID(id);
            return View(game);
        }

        [HttpGet]
        public IActionResult Create()
        {

            CreateGameViewModel model = new() {
                Categories = categoryRepo.GetSelectLists(),
                Devices = DevicesRepo.GetSelectLists()
               
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameViewModel game)
        {
            if(ModelState.IsValid == true)
            {
                await GameRepo.Create(game);
                return RedirectToAction("Index");
            }
            game.Devices = DevicesRepo.GetSelectLists();
            game.Categories = categoryRepo.GetSelectLists();
            return View(game);
        }

        [HttpGet]
        public IActionResult Update(int id) 
        {
            var game = GameRepo.GetByID(id);
            EditGameViewModel model = new EditGameViewModel()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryID = game.CategoryID,
                SelectedDevices = game.GameDevices.Select(d => d.DeviceID).ToList(),
                Categories = categoryRepo.GetSelectLists(),
                Devices = DevicesRepo.GetSelectLists(),
                CurrentCover = game.Cover
                
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EditGameViewModel model)
        {
            if(ModelState.IsValid == true)
            {
               var ga = await GameRepo.Update(model);
                if (ga == null)
                    return BadRequest();
                return RedirectToAction("Index");

            }


            model.Devices = DevicesRepo.GetSelectLists();
            model.Categories = categoryRepo.GetSelectLists();
            return View(model);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = GameRepo.Delete(id);

              // retrn this if is true : Else 
            return isDeleted ? Ok() : BadRequest();
        }
    }
}
