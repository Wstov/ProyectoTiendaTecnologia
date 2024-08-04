using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_SC_601.Data;
using Proyecto_SC_601.Models;
using System.Diagnostics;

namespace Proyecto_SC_601.Controllers
{
    public class CarritoController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<CarritoController> _logger;
        private readonly CarritoHelper _cHelper;

        public CarritoController(ILogger<CarritoController> logger, ApplicationDbContext context, CarritoHelper cHelper)
        {
            _logger = logger;
            _context = context;
            _cHelper = cHelper;
        }

        public async Task<IActionResult> Index()
        {
            var productosActivos = await _context.Productos.Where(p => p.Activo).ToListAsync();
            foreach (var producto in productosActivos)
            {
                var enCarritos = await _cHelper.getCantidadArticulosByProductoID(producto.IDProducto);
                producto.Stock = producto.Stock - enCarritos;
            }

            ViewBag.itemsCount = await _cHelper.getArticulosQTYbyUserMail(User.Identity.Name);

            return View(productosActivos);
        }

        public async Task<IActionResult> IndexFiltro(string nombre)
        {
            var productos = await _context.Productos
                .Where(p => p.NombreProducto.Contains(nombre))
                .ToListAsync();

            return View("Index", productos);
        }

        // POST: / Agregando Detalle
        [HttpPost]
        public async Task<IActionResult> Index(int id, int cantidad)
        {
            var usuarioID = await _cHelper.getUserIdByEmail(User.Identity.Name);
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            var existingCarritoDetalle = await _context.Carritos.FirstOrDefaultAsync(c => c.IDProducto == id && c.IDUsuario == usuarioID);

            if (existingCarritoDetalle != null)
            {
                existingCarritoDetalle.Cantidad += cantidad;
            }
            else
            {
                var car = new Carrito
                {
                    IDProducto = producto.IDProducto,
                    IDUsuario = usuarioID,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.Precio,
                    FechaCreacion = DateTime.UtcNow
                };

                _context.Carritos.Add(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Listado()
        {
            var usuarioID = await _cHelper.getUserIdByEmail(User.Identity.Name);

            var productosEnCarrito = await (from c in _context.Carritos
                                            join p in _context.Productos on c.IDProducto equals p.IDProducto
                                            where c.IDUsuario == usuarioID
                                            select new
                                            {
                                                p.IDProducto,
                                                p.NombreProducto,
                                                p.Descripcion,
                                                c.Cantidad,
                                                c.PrecioUnitario,
                                                p.Stock,
                                                p.ImagenURL1
                                            }).ToListAsync();

            ViewBag.itemsCount = productosEnCarrito.Sum(p => p.Cantidad);
            ViewBag.Subtotal = productosEnCarrito.Sum(p => p.Cantidad * p.PrecioUnitario);
            ViewBag.productosEnCarrito = productosEnCarrito;

            var direcciones = await _context.DireccionesEnvio
                .Where(d => d.IDUsuario == usuarioID)
                .Select(d => new SelectListItem
                {
                    Value = d.IDDireccion.ToString(),
                    Text = d.Direccion
                })
                .ToListAsync();

            ViewBag.DireccionesEnvio = new SelectList(direcciones, "Value", "Text");

            return View();
        }


        // POST: / Modificando Detalle
        [HttpPost]
        public async Task<IActionResult> ListadoModify(int id, int cantidad)
        {
            var usuarioID = await _cHelper.getUserIdByEmail(User.Identity.Name);
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            var existingCarritoDetalle = await _context.Carritos.FirstOrDefaultAsync(c => c.IDProducto == id && c.IDUsuario == usuarioID);

            if (existingCarritoDetalle != null)
            {
                existingCarritoDetalle.Cantidad = cantidad;
            }
            else
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Listado));
        }

        [HttpPost]
        public async Task<IActionResult> ListadoDelete(int id)
        {
            var usuarioID = await _cHelper.getUserIdByEmail(User.Identity.Name);

            var itemEnCarrito = await _context.Carritos.FirstOrDefaultAsync(c => c.IDProducto == id && c.IDUsuario == usuarioID);
            if (itemEnCarrito != null)
            {
                _context.Carritos.Remove(itemEnCarrito);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Listado));
        }

        [HttpPost]
        public async Task<IActionResult> Pago(int IDDireccion)
        {
            if (IDDireccion != 0) 
            { 
                var usuarioID = await _cHelper.getUserIdByEmail(User.Identity.Name);
                var detallesCarrito = await _context.Carritos.Where(c => c.IDUsuario == usuarioID).ToListAsync();
                var pedido = new Pedido
                {
                    IDPedido = 0,
                    IDUsuario = usuarioID,
                    IDDireccion = IDDireccion,
                    FechaPedido = DateTime.UtcNow,
                    Total = detallesCarrito.Sum(p => p.Cantidad * p.PrecioUnitario) * 1.13m,
                    Estado = "Pagado"
                };

                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();
                int generatedID = pedido.IDPedido;
                var producto = new Producto();

                foreach (var detalleCarrito in detallesCarrito)
                {
                    producto = await _context.Productos.FindAsync(detalleCarrito.IDProducto);
                    if (producto != null)
                    {
                        var detallePedido = new DetallePedido
                        {
                            IDDetalle = 0,
                            IDPedido = generatedID,
                            IDProducto = producto.IDProducto,
                            Cantidad = detalleCarrito.Cantidad,
                            PrecioUnitario = detalleCarrito.PrecioUnitario
                        };
                        _context.DetallesPedidos.Add(detallePedido);
                        await _context.SaveChangesAsync();

                        producto.Stock = producto.Stock - detalleCarrito.Cantidad;
                        _context.Update(producto);
                        await _context.SaveChangesAsync();
                    }
                }

                var carritos = await _context.Carritos.Where(c => c.IDUsuario == usuarioID).ToListAsync();
                _context.Carritos.RemoveRange(carritos);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Listado));
        }

        public async Task<IActionResult> DetalleProducto(int id)
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

            ViewBag.itemsCount = await _cHelper.getArticulosQTYbyUserMail(User.Identity.Name);
            return View(objModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetItemsCount()
        {
            var itemsCount = await _cHelper.getArticulosQTYbyUserMail(User.Identity.Name);
            return Json(new { count = itemsCount });
        }

    }
}
