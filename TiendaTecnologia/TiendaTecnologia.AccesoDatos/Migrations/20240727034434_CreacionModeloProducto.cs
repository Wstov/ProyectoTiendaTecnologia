using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaTecnologia.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreacionModeloProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Idproducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Idcategoria = table.Column<int>(type: "int", nullable: false),
                    Idproveedor = table.Column<int>(type: "int", nullable: false),
                    ImagenUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdcategoriaNavigationIdcategoria = table.Column<int>(type: "int", nullable: false),
                    IdproveedorNavigationIdproveedor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Idproducto);
                    table.ForeignKey(
                        name: "FK_Producto_Categoria_IdcategoriaNavigationIdcategoria",
                        column: x => x.IdcategoriaNavigationIdcategoria,
                        principalTable: "Categoria",
                        principalColumn: "Idcategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Producto_Proveedor_IdproveedorNavigationIdproveedor",
                        column: x => x.IdproveedorNavigationIdproveedor,
                        principalTable: "Proveedor",
                        principalColumn: "Idproveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Producto_IdcategoriaNavigationIdcategoria",
                table: "Producto",
                column: "IdcategoriaNavigationIdcategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_IdproveedorNavigationIdproveedor",
                table: "Producto",
                column: "IdproveedorNavigationIdproveedor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Producto");
        }
    }
}
