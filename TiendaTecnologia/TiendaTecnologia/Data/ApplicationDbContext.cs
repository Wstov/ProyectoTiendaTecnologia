using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TiendaTecnologia.Models;

namespace TiendaTecnologia.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<DetalleCarrito> DetalleCarritos { get; set; }

    public virtual DbSet<DetallesPedido> DetallesPedidos { get; set; }

    public virtual DbSet<DireccionesEnvio> DireccionesEnvios { get; set; }

    public virtual DbSet<MetodosPago> MetodosPagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedores { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-MGA13MF;Database=TiendaTecnologia;Integrated Security=True;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.Idcarrito).HasName("PK__Carrito__C8EB8D92CDB053F8");

            entity.ToTable("Carrito");

            entity.Property(e => e.Idcarrito).HasColumnName("IDCarrito");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Carrito__IDUsuar__49C3F6B7");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Idcategoria).HasName("PK__Categori__70E82E28B0EBC781");

            entity.Property(e => e.Idcategoria).HasColumnName("IDCategoria");
            entity.Property(e => e.NombreCategoria).HasMaxLength(255);
        });

        modelBuilder.Entity<DetalleCarrito>(entity =>
        {
            entity.HasKey(e => e.IddetalleCarrito).HasName("PK__DetalleC__2B49305FAF21BD71");

            entity.ToTable("DetalleCarrito");

            entity.Property(e => e.IddetalleCarrito).HasColumnName("IDDetalleCarrito");
            entity.Property(e => e.Idcarrito).HasColumnName("IDCarrito");
            entity.Property(e => e.Idproducto).HasColumnName("IDProducto");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdcarritoNavigation).WithMany(p => p.DetalleCarritos)
                .HasForeignKey(d => d.Idcarrito)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleCa__IDCar__4CA06362");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.DetalleCarritos)
                .HasForeignKey(d => d.Idproducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleCa__IDPro__4D94879B");
        });

        modelBuilder.Entity<DetallesPedido>(entity =>
        {
            entity.HasKey(e => e.Iddetalle).HasName("PK__Detalles__32EB9E47451AECB3");

            entity.Property(e => e.Iddetalle).HasColumnName("IDDetalle");
            entity.Property(e => e.Idpedido).HasColumnName("IDPedido");
            entity.Property(e => e.Idproducto).HasColumnName("IDProducto");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdpedidoNavigation).WithMany(p => p.DetallesPedidos)
                .HasForeignKey(d => d.Idpedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallesP__IDPed__45F365D3");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.DetallesPedidos)
                .HasForeignKey(d => d.Idproducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallesP__IDPro__46E78A0C");
        });

        modelBuilder.Entity<DireccionesEnvio>(entity =>
        {
            entity.HasKey(e => e.Iddireccion).HasName("PK__Direccio__2BA9D38411FB1E00");

            entity.ToTable("DireccionesEnvio");

            entity.Property(e => e.Iddireccion).HasColumnName("IDDireccion");
            entity.Property(e => e.Ciudad).HasMaxLength(100);
            entity.Property(e => e.CodigoPostal).HasMaxLength(20);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");
            entity.Property(e => e.Pais).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.DireccionesEnvios)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Direccion__IDUsu__5070F446");
        });

        modelBuilder.Entity<MetodosPago>(entity =>
        {
            entity.HasKey(e => e.IdmetodoPago).HasName("PK__MetodosP__8D99F338A57EDE94");

            entity.ToTable("MetodosPago");

            entity.Property(e => e.IdmetodoPago).HasColumnName("IDMetodoPago");
            entity.Property(e => e.FechaExpiracion).HasColumnType("datetime");
            entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");
            entity.Property(e => e.NumeroCuenta).HasMaxLength(50);
            entity.Property(e => e.Proveedor).HasMaxLength(255);
            entity.Property(e => e.Tipo).HasMaxLength(50);

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.MetodosPagos)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MetodosPa__IDUsu__534D60F1");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Idpedido).HasName("PK__Pedidos__00C11F99D6495B6A");

            entity.Property(e => e.Idpedido).HasColumnName("IDPedido");
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.FechaPedido).HasColumnType("datetime");
            entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedidos__IDUsuar__4316F928");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Idproducto).HasName("PK__Producto__ABDAF2B495EBB5A8");

            entity.Property(e => e.Idproducto).HasColumnName("IDProducto");
            entity.Property(e => e.Idcategoria).HasColumnName("IDCategoria");
            entity.Property(e => e.Idproveedor).HasColumnName("IDProveedor");
            entity.Property(e => e.ImagenUrl).HasColumnName("ImagenURL");
            entity.Property(e => e.NombreProducto).HasMaxLength(255);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdcategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Idcategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__IDCat__3F466844");

            entity.HasOne(d => d.IdproveedorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Idproveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__IDPro__403A8C7D");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.Idproveedor).HasName("PK__Proveedo__4CD73240B8D215CA");

            entity.Property(e => e.Idproveedor).HasColumnName("IDProveedor");
            entity.Property(e => e.Ciudad).HasMaxLength(100);
            entity.Property(e => e.Contacto).HasMaxLength(255);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.NombreProveedor).HasMaxLength(255);
            entity.Property(e => e.Pais).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PK__Usuarios__52311169405AB396");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D10534F0D152AE").IsUnique();

            entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Ciudad).HasMaxLength(100);
            entity.Property(e => e.CodigoPostal).HasMaxLength(20);
            entity.Property(e => e.Contraseña).HasMaxLength(255);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Pais).HasMaxLength(100);
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .HasDefaultValue("user");
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
