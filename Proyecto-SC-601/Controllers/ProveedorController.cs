using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_SC_601.Data;
using Proyecto_SC_601.Models;
using System.Diagnostics;

namespace Proyecto_SC_601.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProveedorController> _logger;

        public ProveedorController(ILogger<ProveedorController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Listado()
        {
            return View(await _context.Proveedores.ToListAsync());
        }

        //GET: Model/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var objModel = await _context.Proveedores.FirstOrDefaultAsync(m => m.IDProveedor == id);

            if (objModel == null)
            {
                return NotFound();
            }

            return View(objModel);
        }

        // GET: Model/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Model/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Proveedor objModel)
        {
            if (ModelState.IsValid)
            {
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

            var objModel = await _context.Proveedores.FindAsync(id);

            if (objModel == null)
            {
                return NotFound();
            }

            return View(objModel);
        }

        private bool Exists(int id)
        {
            return _context.Proveedores.Any(e => e.IDProveedor == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Proveedor objModel)
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
                    if (!Exists(objModel.IDProveedor))
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
            if (id == 0)
            {
                return NotFound();
            }

            var objModel = await _context.Proveedores.FirstOrDefaultAsync(m => m.IDProveedor == id);

            if (objModel == null)
            {
                return NotFound();
            }

            return View(objModel);
        }

        public async Task<IActionResult> DeleteConfirmed(int IDProveedor)
        {
            var objModel = await _context.Proveedores.FindAsync(IDProveedor);
            _context.Proveedores.Remove(objModel);
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

