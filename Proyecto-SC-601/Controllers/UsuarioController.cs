using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Proyecto_SC_601.Data;
using System;
using Proyecto_SC_601.Models;

namespace Proyecto_SC_601.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Listado()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        //GET: Products/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var objModel = await _context.Usuarios.FirstOrDefaultAsync(m => m.IDUsuario == id);

            if (objModel == null)
            {
                return NotFound();
            }

            return View(objModel);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario objModel)
        {
            if (ModelState.IsValid)
            {
                objModel.Contraseña = BCrypt.Net.BCrypt.HashPassword(objModel.Contraseña);
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

            var objModel = await _context.Usuarios.FindAsync(id);

            if (objModel == null)
            {
                return NotFound();
            }


            objModel.Contraseña = string.Empty;
            return View(objModel);
        }

        private bool Exists(int id)
        {
            return _context.Usuarios.Any(e => e.IDUsuario == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Usuario objModel)
        {
            if (ModelState.IsValid)
            {
                objModel.Contraseña = BCrypt.Net.BCrypt.HashPassword(objModel.Contraseña);
                try
                {
                    _context.Update(objModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Exists(objModel.IDUsuario))
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

        // GET: Product/Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var objModel = await _context.Usuarios.FirstOrDefaultAsync(m => m.IDUsuario == id);

            if (objModel == null)
            {
                return NotFound();
            }

            return View(objModel);
        }

        public async Task<IActionResult> DeleteConfirmed(int IDUsuario)
        {
            var objModel = await _context.Usuarios.FindAsync(IDUsuario);
            _context.Usuarios.Remove(objModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Listado));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
