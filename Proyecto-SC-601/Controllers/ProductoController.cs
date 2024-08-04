using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_SC_601.Data;
using Proyecto_SC_601.Models;
using System.Diagnostics;

namespace Proyecto_SC_601.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(ILogger<ProductoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Listado()
        {
            return View(await _context.Productos.ToListAsync());
        }

        //GET: Model/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var objModel = await _context.Productos.FirstOrDefaultAsync(m => m.IDProducto == id);

            if (objModel == null)
            {
                return NotFound();
            }

            var categorias = _context.Categorias.ToList();
            var proveedores = _context.Proveedores.ToList();
            ViewBag.NombreCategoria = categorias.FirstOrDefault(c => c.IDCategoria == objModel.IDCategoria)?.NombreCategoria;
            ViewBag.NombreProveedor = proveedores.FirstOrDefault(p => p.IDProveedor == objModel.IDProveedor)?.NombreProveedor;

            return View(objModel);
        }

        // GET: Model/Create
        public IActionResult Create()
        {
            ViewBag.Categorias = new SelectList(_context.Categorias, "IDCategoria", "NombreCategoria");
            ViewBag.Proveedores = new SelectList(_context.Proveedores, "IDProveedor", "NombreProveedor");
            return View();
        }

        // POST: Model/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto objModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(objModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Listado));
            }
            ViewBag.Categorias = new SelectList(_context.Categorias, "IDCategoria", "NombreCategoria", objModel.IDCategoria);
            ViewBag.Proveedores = new SelectList(_context.Proveedores, "IDProveedor", "NombreProveedor", objModel.IDProveedor);
            return View(objModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var objModel = await _context.Productos.FindAsync(id);

            if (objModel == null)
            {
                return NotFound();
            }

            ViewBag.Categorias = new SelectList(_context.Categorias, "IDCategoria", "NombreCategoria");
            ViewBag.Proveedores = new SelectList(_context.Proveedores, "IDProveedor", "NombreProveedor");
            return View(objModel);
        }

        private bool Exists(int id)
        {
            return _context.Productos.Any(e => e.IDProducto == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Producto objModel)
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
                    if (!Exists(objModel.IDProducto))
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
            ViewBag.Categorias = new SelectList(_context.Categorias, "IDCategoria", "NombreCategoria", objModel.IDCategoria);
            ViewBag.Proveedores = new SelectList(_context.Proveedores, "IDProveedor", "NombreProveedor", objModel.IDProveedor);
            return View(objModel);
        }

        // GET: Model/Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var objModel = await _context.Productos.FirstOrDefaultAsync(m => m.IDProducto == id);

            if (objModel == null)
            {
                return NotFound();
            }

            var categorias = _context.Categorias.ToList();
            var proveedores = _context.Proveedores.ToList();
            ViewBag.NombreCategoria = categorias.FirstOrDefault(c => c.IDCategoria == objModel.IDCategoria)?.NombreCategoria;
            ViewBag.NombreProveedor = proveedores.FirstOrDefault(p => p.IDProveedor == objModel.IDProveedor)?.NombreProveedor;

            return View(objModel);
        }

        public async Task<IActionResult> DeleteConfirmed(int IDProducto)
        {
            var objModel = await _context.Productos.FindAsync(IDProducto);
            _context.Productos.Remove(objModel);
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
