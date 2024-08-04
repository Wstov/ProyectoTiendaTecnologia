using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_SC_601.Data;
using Proyecto_SC_601.Models;
using System.Diagnostics;

namespace Proyecto_SC_601.Controllers
{
    public class DireccionEnvioController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DireccionEnvioController> _logger;
        private readonly CarritoHelper _cHelper;

        public DireccionEnvioController(ILogger<DireccionEnvioController> logger, ApplicationDbContext context, CarritoHelper cHelper)
        {
            _logger = logger;
            _context = context;
            _cHelper = cHelper;
        }

        public async Task<IActionResult> Listado()
        {
            ViewBag.itemsCount = await _cHelper.getArticulosQTYbyUserMail(User.Identity.Name);
            return View(await _context.DireccionesEnvio.ToListAsync());  
        }

        //GET: Model/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objModel = await _context.DireccionesEnvio.FirstOrDefaultAsync(m => m.IDDireccion == id);

            if (objModel == null)
            {
                return NotFound();
            }

            ViewBag.itemsCount = await _cHelper.getArticulosQTYbyUserMail(User.Identity.Name);
            return View(objModel);
        }

        // GET: Model/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.itemsCount = await _cHelper.getArticulosQTYbyUserMail(User.Identity.Name);
            return View();
        }

        // POST: Model/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DireccionEnvio objModel)
        {
            if (ModelState.IsValid)
            {
                objModel.IDUsuario = await _cHelper.getUserIdByEmail(User.Identity.Name);
                _context.Add(objModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Listado));
            }
            return View(objModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var objModel = await _context.DireccionesEnvio.FindAsync(id);

            if (objModel == null)
            {
                return NotFound();
            }

            return View(objModel);
        }

        private bool Exists(int id)
        {
            return _context.DireccionesEnvio.Any(e => e.IDDireccion == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DireccionEnvio objModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(objModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Exists(objModel.IDDireccion))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Listado));
            }
            return View(objModel);
        }

        // GET: Model/Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objModel = await _context.DireccionesEnvio.FirstOrDefaultAsync(m => m.IDDireccion == id);

            if (objModel == null)
            {
                return NotFound();
            }

            return View(objModel);
        }

        public async Task<IActionResult> DeleteConfirmed(int IDDireccion)
        {
            var objModel = await _context.DireccionesEnvio.FindAsync(IDDireccion);
            _context.DireccionesEnvio.Remove(objModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Listado));
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
