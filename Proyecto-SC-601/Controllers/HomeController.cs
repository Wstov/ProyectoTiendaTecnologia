using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_SC_601.Models;
using System.Diagnostics;
using Proyecto_SC_601.Data;

namespace Proyecto_SC_601.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly CarritoHelper _cHelper;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, CarritoHelper cHelper)
        {
            _logger = logger;
            _context = context;
            _cHelper = cHelper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.itemsCount = await _cHelper.getArticulosQTYbyUserMail(User.Identity.Name);
            return View();
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
