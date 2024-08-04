using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_SC_601.Models;
using System.Diagnostics;
using Proyecto_SC_601.Data;
using Proyecto_SC_601.Controllers;

namespace Proyecto_SC_601.Data
{
    public class CarritoHelper
    {
        private readonly ApplicationDbContext _context;

        public CarritoHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> getUserIdByEmail(string email)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (usuario != null)
            {
                return usuario.IDUsuario;
            }
            return 0;
        }

        private async Task<int> getCantidadArticulos(int userID)
        {
            if (userID != 0)
            {
                var totalCantidad = await _context.Carritos
                    .Where(c => c.IDUsuario == userID)
                    .SumAsync(c => c.Cantidad);

                return totalCantidad;
            }
            return 0;
        }

        public async Task<int> getCantidadArticulosByProductoID(int productoID)
        {
            if (productoID != 0)
            {
                var totalCantidad = await _context.Carritos
                    .Where(c => c.IDProducto == productoID)
                    .SumAsync(c => c.Cantidad);

                return totalCantidad;
            }
            return 0;
        }

        public async Task<int> getArticulosQTYbyUserMail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var id = await getUserIdByEmail(email);
                return await getCantidadArticulos(id);
            }
            return 0;
        }
    }
}
