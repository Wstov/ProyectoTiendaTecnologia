using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_SC_601.Data;
using Proyecto_SC_601.Models;
using System.Diagnostics;
using System;

namespace Proyecto_SC_601.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(ILogger<CategoriaController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Listado()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        //GET: Model/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var objModel = await _context.Categorias.FirstOrDefaultAsync(m => m.IDCategoria == id);

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
        public async Task<IActionResult> Create(Categoria objModel)
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

            var objModel = await _context.Categorias.FindAsync(id);

            if (objModel == null)
            {
                return NotFound();
            }

            return View(objModel);
        }

        private bool Exists(int id)
        {
            return _context.Categorias.Any(e => e.IDCategoria == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Categoria objModel)
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
                    if (!Exists(objModel.IDCategoria))
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

            var objModel = await _context.Categorias.FirstOrDefaultAsync(m => m.IDCategoria == id);

            if (objModel == null)
            {
                return NotFound();
            }

            return View(objModel);
        }

        public async Task<IActionResult> DeleteConfirmed(int IDCategoria)
        {
            var objModel = await _context.Categorias.FindAsync(IDCategoria);
            _context.Categorias.Remove(objModel);
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
