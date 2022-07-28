using ECommProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ECommProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ProjectContext context;
        public HomeController(ILogger<HomeController> logger, ProjectContext context1)
        {
            _logger = logger;
            context = context1;
            
        }

        public IActionResult Index()
        {
            return View(context.product.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}