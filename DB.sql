/*CREAMOS LA BASE DE DATOS PARA LA INTEGRACIÓN Y USO DEL CRUD*/
/*JUNIO 20 DE 2024*/
/*ELOBARADO POR BRYAN QUIROGA*/

create database MiIngresoHitss;

use database;

CREATE TABLE Clientes (
    ClienteId INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100),
    Email VARCHAR(100),
    Telefono VARCHAR(15)
);

CREATE TABLE Productos (
    ProductoId INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100),
    Descripcion VARCHAR(255),
    Precio DECIMAL(18, 2),
    Stock INT
);

CREATE TABLE ListasDePrecios (
    ListaPrecioId INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100)
);

CREATE TABLE ProductoListaPrecio (
    ProductoId INT,
    ListaPrecioId INT,
    Precio DECIMAL(18, 2),
    PRIMARY KEY (ProductoId, ListaPrecioId),
    FOREIGN KEY (ProductoId) REFERENCES Productos(ProductoId),
    FOREIGN KEY (ListaPrecioId) REFERENCES ListasDePrecios(ListaPrecioId)
);

CREATE TABLE Ventas (
    VentaId INT PRIMARY KEY IDENTITY(1,1),
    ClienteId INT,
    Fecha DATETIME,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(ClienteId)
);

CREATE TABLE DetallesVenta (
    VentaId INT,
    ProductoId INT,
    Cantidad INT,
    Precio DECIMAL(18, 2),
    PRIMARY KEY (VentaId, ProductoId),
    FOREIGN KEY (VentaId) REFERENCES Ventas(VentaId),
    FOREIGN KEY (ProductoId) REFERENCES Productos(ProductoId)
);

/* SEGUNDO PASO: PROCEDIMIENTOS ALMACENADOS*/

CREATE PROCEDURE ObtenerVentasPorFecha
	@FechaInicio DATATIME,
	@FechaFin DATATIME
	AS
	BEGIN
		SELECT v.VentaId, v.Fecha, c.Nombre AS Cliente, p.Nombre AS Producto, dv.Cantidad, dv.Precio
		FROM Ventas v
		JOIN DetallesVenta dv ON v.VentaId = dv.VentaId
		JOIN Clientes c ON v.ClienteId = c.ClienteId
    JOIN Productos p ON dv.ProductoId = p.ProductoId
    WHERE v.Fecha BETWEEN @FechaInicio AND @FechaFin
    ORDER BY v.Fecha;
END;
