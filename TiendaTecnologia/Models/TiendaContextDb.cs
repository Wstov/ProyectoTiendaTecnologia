using Microsoft.EntityFrameworkCore;

namespace TiendaTecnologia.Models
{
    public class TiendaContextDb : DbContext
    {
        public TiendaContextDb(DbContextOptions<TiendaContextDb> options) : base(options)
        {

        }

    }
}
