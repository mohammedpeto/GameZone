using GameZone.Models;
using GameZone.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameRepo gameRepo;

        public HomeController(ILogger<HomeController> logger , IGameRepo _gameRepo)
        {
            _logger = logger;
            gameRepo = _gameRepo;
        }

        public IActionResult Index()
        {
            var games = gameRepo.GetAll();
            return View(games);
        }

     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
