CREATE DATABASE TiendaTecnologia;
GO

USE TiendaTecnologia;
GO

-- Usuarios
CREATE TABLE Usuarios (
    IDUsuario INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Contraseña NVARCHAR(255) NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    Direccion NVARCHAR(255),
    Ciudad NVARCHAR(100),
    Pais NVARCHAR(100),
    CodigoPostal NVARCHAR(20),
    Telefono NVARCHAR(20),
    Rol NVARCHAR(50) NOT NULL,
);
GO

-- Categorias
CREATE TABLE Categorias (
    IDCategoria INT PRIMARY KEY IDENTITY(1,1),
    NombreCategoria NVARCHAR(255) NOT NULL,
    Descripcion NVARCHAR(MAX)
);
GO

-- Proveedores
CREATE TABLE Proveedores (
    IDProveedor INT PRIMARY KEY IDENTITY(1,1),
    NombreProveedor NVARCHAR(255) NOT NULL,
    Contacto NVARCHAR(255),
    Direccion NVARCHAR(255),
    Ciudad NVARCHAR(100),
    Pais NVARCHAR(100),
    Telefono NVARCHAR(20),
    Email NVARCHAR(255)
);
GO

-- Productos
CREATE TABLE Productos (
    IDProducto INT PRIMARY KEY IDENTITY(1,1),
    NombreProducto NVARCHAR(255) NOT NULL,
    Descripcion NVARCHAR(MAX),
    Precio DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL,
    IDCategoria INT NOT NULL,
    IDProveedor INT NOT NULL,
    ImagenURL1 NVARCHAR(MAX),
    ImagenURL2 NVARCHAR(MAX),
    ImagenURL3 NVARCHAR(MAX),
    Activo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (IDCategoria) REFERENCES Categorias(IDCategoria),
    FOREIGN KEY (IDProveedor) REFERENCES Proveedores(IDProveedor)
);
GO

-- Pedidos
CREATE TABLE Pedidos (
    IDPedido INT PRIMARY KEY IDENTITY(1,1),
    IDUsuario INT NOT NULL,
    IDDireccion INT NOT NULL,
    FechaPedido DATETIME NOT NULL,
    Total DECIMAL(10, 2) NOT NULL,
    Estado NVARCHAR(50),
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),
    FOREIGN KEY (IDDireccion) REFERENCES DireccionesEnvio(IDDireccion)
);
GO

-- Crear tabla DetallesPedidos
CREATE TABLE DetallesPedidos (
    IDDetalle INT PRIMARY KEY IDENTITY(1,1),
    IDPedido INT NOT NULL,
    IDProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (IDPedido) REFERENCES Pedidos(IDPedido),
    FOREIGN KEY (IDProducto) REFERENCES Productos(IDProducto)
);
GO

-- Crear tabla Carritos
CREATE TABLE Carrito (
    IDDetalle INT PRIMARY KEY IDENTITY(1,1),
    IDProducto INT NOT NULL,
    IDUsuario INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10, 2) NOT NULL,
    FechaCreacion DATETIME NOT NULL,
    FOREIGN KEY (IDProducto) REFERENCES Productos(IDProducto),
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario),
);
GO

-- Crear tabla DireccionesEnvio
CREATE TABLE DireccionesEnvio (
    IDDireccion INT PRIMARY KEY IDENTITY(1,1),
    IDUsuario INT NOT NULL,
    Direccion NVARCHAR(255) NOT NULL,
    Ciudad NVARCHAR(100),
    Pais NVARCHAR(100),
    CodigoPostal NVARCHAR(20),
    Telefono NVARCHAR(20),
    FOREIGN KEY (IDUsuario) REFERENCES Usuarios(IDUsuario)
);
GO

CREATE PROCEDURE [dbo].[sp_DeleteOldRecords]
AS
BEGIN
    DELETE FROM Carritos
    WHERE FechaCreacion < DATEADD(MINUTE, -30, getUTCDATE())
END;
GO