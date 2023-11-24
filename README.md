# GARDENS: Jardineria y Vivero

![](./Img/ProyectoJardineria.jpg)

La empresa Gardens especializada en Jardineria desea construir una aplicacion que le permita llevar el control
y registro de todos sus productos y servicios.

**Objetives**🎯
1. 1. Generar el Proyecto Backend usando DBFirst o CodeFirst.
2. Organizar el Proyecto usando 4 Capas.
3. Genere reppositorio para el Proyecto. El enlace del repositorio debe ser enviado por medio del correo electronico Johlverpardo.campuslands@gmail.com. Estructura del Envio:

    Asunto : Enlace filtro Nombres_Apellidos y Grupo.
    Cuerpo : Nombres, Apellidos y Grupo

4. Se debe generar el README del proyecto teniendo en cuenta las siguientes especificaciones.

    Enunciado de la consulta
    EndPoint de la Consulta
    Codigo de la consulta
    Explicacion de la consulta

4. Fecha de entrega : 23 Noviembre al finalizar el Skill
5. Tiempo estimado de la prueba : 4 hras


[![GitHub](https://badgen.net/badge/icon/github?icon=github&label)](https://github.com)
[![.NET](https://img.shields.io/badge/--512BD4?logo=.net&logoColor=ffffff)](https://dotnet.microsoft.com/)
[![NuGet](https://badgen.net/badge/icon/nuget?icon=nuget&label)](https://https://nuget.org/)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://learn.microsoft.com/es-es/dotnet/csharp/)
[![GitHub](https://img.shields.io/badge/MySQL-005C84?style=for-the-badge&logo=mysql&logoColor=white)](https://www.mysql.com/)

---
**Table of Contents**📑

[TOCM]

[TOC]



### Endpoints Especificos ✅
Para el desarrollo de las consultas se analizaron las variables de estas para dar flexibilidad al desarrollo de estas.
**Método**: `GET`



#### 1. Devuelve un listado con el codigo de pedid, codigo de cliente, fecha esperada y fecha de entrega de los pediods que no han sio entregados a tiempo

**Endpoint**: `http://localhost:5295/api/Pedido/GetC1`
**Consulta**
```c#
var result = await (
            from p in _context.Pedidos
            where p.FechaEsperada != p.FechaEntrega
            select new
            {
                IdPedido = p.Id,
                IdCliente = p.IdCliente,
                FechaEsperada = p.FechaEsperada,
                FechaEntrega = p.FechaEntrega,
            }
        ).ToListAsync();
        return result;
```
En esta consulta se selecionan los registros de la tabla pedidos, luego se filtran cuando la fecha de esperada y de entrega son diferentes


#### 2. Devuelve el nombre de los clientes que no hayan hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina a la que pertenece el representante.

**Endpoint**: `http://localhost:5295/api/Cliente/GetC2`
**Consulta**
```c#
var result = await (
            from c in _context.Clientes
            join e in _context.Empleados on c.IdEmpleadoRepVentas equals e.Id
            where !_context.Pagos.Any(pg => pg.IdCliente == c.Id)
            join o in _context.Oficinas on e.IdOficina equals o.Id
            select new
            {
                IdCliente = c.Id,
                Cliente = c.NombreCliente,
                NombreRepresentanteVentas = e.Nombre,
                ApellidoRepresentanteVentas1 = e.Apellidol,
                ApellidoRepresentanteVentas2 = e.Apellido2,
                OficinaRepresentante = o.Id,
                CiudadOficina = o.Ciudad
            }
        )
        .Distinct()
        .ToListAsync();
        return result;
```
En esta consulta se seleccionan la tabla cliente, se hace un join con empleados y se le aplica un left join con la tabla pagos para devolver los registros que no estan presentes en la anterior, luego se aplica un join oficina para retornar los valores solicitados


#### 3. Devuelve las oficinas donde no trabajan ninguno de los empleados que hayan sido los representantes de ventas de algún cliente que haya realizado la compra de algún producto de la gama Frutales.

**Endpoint**: `http://localhost:5295/api/Oficina/GetC3`
**Consulta**
```c#
var result = await (
            from o in _context.Oficinas
            join e in _context.Empleados on o.Id equals e.IdOficina
            join c in _context.Clientes on e.Id equals c.IdEmpleadoRepVentas
            join p in _context.Pedidos on c.Id equals p.IdCliente
            join dp in _context.DetallesPedidos on p.Id equals dp.IdPedido
            join pd in _context.Productos on dp.IdProducto equals pd.Id
            where pd.Gama != "Frutales"
            select o
        )
        .ToListAsync();
        return result;
```
En esta consulta se selecciona la tabla oficina se le aplica los joins para acceder hasta la tabla productos donde se filtran los diferentes a la gama Frutales y se devulve las oficinas

#### 4. Devuelve un listado de los 20 productos más vendidos y el número total de unidades que se han vendido de cada uno. El listado deberá estar ordenado por el número total de unidades vendidas.

**Endpoint**: `http://localhost:5295/api/Producto/GetC4`
**Consulta**
```c#
var result = await (
            from dp in _context.DetallesPedidos
            join p in _context.Productos on dp.IdProducto equals p.Id
            group dp by new { dp.IdProducto, p.Nombre } into g
            select new
            {
                IdProducto = g.Key.Nombre,
                TotalUnidadesVendidas = g.Sum(dp => dp.Cantidad)
            }
        )
        .OrderByDescending(p => p.TotalUnidadesVendidas)
        .Take(20)
        .ToListAsync();
        return result;
```
En esta consulta se selecciona la detallesPedidos, posterior mente se le hace un inner join con productos se agrupan para devolver y realizar la cuenta de los productos para poder selecionar los 20 primeros luego de ordenarlos en funcion de esta ultima propiedad


#### 5. Lista las ventas totales de los productos que hayan facturado más de 3000 euros. Se mostrará el nombre, unidades vendidas, total facturado y total facturado con impuestos (21% IVA).

**Endpoint**: `http://localhost:5295/api/Producto/GetC5`
**Consulta**
```c#
var result = await (
            from dp in _context.DetallesPedidos
            join p in _context.Productos on dp.IdProducto equals p.Id
            group new { dp, p } by dp.IdProducto into g
            select new
            {
                IdProducto = g.Key,
                NombreProducto = g.Select(x => x.p.Nombre).FirstOrDefault(),
                TotalUnidadesVendidas = g.Sum(x => x.dp.Cantidad),
                TotalFacturado = g.Sum(x => x.dp.Cantidad * x.dp.PrecioUnidad),
                TotalFacturadoIVA = g.Sum(x => x.dp.Cantidad * x.dp.PrecioUnidad * Convert.ToDecimal(1.21))
            }
        )
        .Where(p => p.TotalFacturado > 3000)
        .OrderByDescending(p => p.TotalFacturado)
        .ToListAsync();
        return result;
```
En esta consulta se selecciona detallesPedidos, se une a productos, se agrupan en funcion del id de producto para posteriormente realizar los calculos solicitados y devolver las que tengan un valor de total facturado superior a 3000 de forma ordenada

#### 6. Devuelve el nombre del producto del que se han vendido más unidades. (Tenga en cuenta que tendrá que calcular cuál es el número total de unidades que se han vendido de cada producto a partir de los datos de la tabla detalle_pedido).

**Endpoint**: `http://localhost:5295/api/Producto/GetC6`
**Consulta**
```c#
var result = await (
            from dp in _context.DetallesPedidos
            join p in _context.Productos on dp.IdProducto equals p.Id
            group dp by new { p.Nombre } into g
            select new
            {
                IdProducto = g.Key.Nombre,
                TotalUnidadesVendidas = g.Sum(dp => dp.Cantidad)
            }
        )
        .OrderByDescending(p => p.TotalUnidadesVendidas)
        .Take(1)
        .ToListAsync();
        return result;
```
En esta consulta se realiza un proceso similar al anterior solo que en esta seleccionamos solo el primer elemento posterior a ser ordenado

#### 7. Devuelve el listado de clientes indicando el nombre del cliente y cuántos pedidos ha realizado. Tenga en cuenta que pueden existir clientes que no han realizado ningún pedido.

**Endpoint**: `http://localhost:5295/api/Cliente/GetC7`
**Consulta**
```c#
var result = await (
            from c in _context.Clientes
            orderby c.Id
            select new
            {
                Id = c.Id,
                Nombre = c.NombreCliente,
                Pedidos = (
                    from p in _context.Pedidos
                    where p.IdCliente == c.Id
                    select p
                    ).Count()
            }
        )
        .ToListAsync();
        return result;
```
En esta consulta se selecciona los cliente y se realiza una subconsulta para poder devolver los pedidos que su id de cliente es igual al de la consulta principal

#### 8. Devuelve el listado de clientes donde aparezca el nombre del cliente, el nombre y primer apellido de su representante de ventas y la ciudad donde está su oficina.

**Endpoint**: `http://localhost:5295/api/Cliente/GetC8`
**Consulta**
```c#
var result = await (
            from c in _context.Clientes
            join e in _context.Empleados on c.IdEmpleadoRepVentas equals e.Id
            where !_context.Pagos.Any(pg => pg.IdCliente == c.Id)
            join o in _context.Oficinas on e.IdOficina equals o.Id
            select new
            {
                IdCliente = c.Id,
                Cliente = c.NombreCliente,
                NombreRepresentanteVentas = e.Nombre,
                ApellidoRepresentanteVentas1 = e.Apellidol,
                ApellidoRepresentanteVentas2 = e.Apellido2,
                OficinaRepresentante = o.Id,
                CiudadOficina = o.Ciudad
            }
        )
        .Distinct()
        .ToListAsync();
        return result;
```
En esta Se seleciona clientes junto a su representante de ventas y se seleccion los que registros que no se relacionen en la tabla de pagos y se devuelve con los datos solicitados


#### 9. Devuelve un listado con los datos de los empleados que no tienen clientes asociados y el nombre de su jefe asociado.

**Endpoint**: `http://localhost:5295/api/Empleado/GetC9`
**Consulta**
```c#
var result = await (
            from e in _context.Empleados
            join j in _context.Empleados on e.IdJefe equals j.Id
            where !_context.Clientes.Any(c => c.IdEmpleadoRepVentas == e.Id)
            select new
            {
                IdEmpleado = e.Id,
                Nombre = e.Nombre,
                Apellido1 = e.Apellidol,
                Apellido2 = e.Apellido2,
                IdJefe = j.Id,
                NombreJefe = j.Nombre,
                Apellido1Jefe = j.Apellidol,
                Apellido2Jefe = j.Apellido2,
            }
        )
        .ToListAsync();
        return result;
```
En esta se selecciona los emplesados junto a su jefe por una subconsulta sencilla y posteriormente mente se le aplica un left join a clientes y se devulven los datos solicitados de forma coherente


#### 10. Devuelve un listado de los productos que nunca han aparecido en un pedido. El resultado debe mostrar el nombre, la descripción y la imagen del producto.

**Endpoint**: `http://localhost:5295/api/Producto/GetC10`
**Consulta**
```c#
var result = await (
            from p in _context.Productos
            join dp in _context.DetallesPedidos on p.Id equals dp.IdProducto
            where !_context.Pedidos.Any(pe => pe.Id == dp.IdPedido)
            join g in _context.GamasProductos on p.Gama equals g.Id
            select new
            {
                IdPRoducto = p.Id,
                NombreProducto = p.Nombre,
                Descripcion = p.Descripcion,
                Imagen = g.Imagen
            }
        )
        .Distinct()
        .ToListAsync();
        return result;
```
En estra se sleciona la tabla productos se le aplica un inner join a detalles pedidos  luego se filtra a los que no esten en la talba pedidos, posteriormente se realiza otro innerjoin a gamaproductos para poder traer los demas datos solicitados 


---
## Tecnologias 💻

-   NetCore 7.0
-   MySQL
-   GitHub

---
### Lenguajes Usados 💬
>

-   C#

---
### Dependencias Usadas 📦
>

-   "AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.1"
-   "Microsoft.AspNetCore.OpenApi" Version="7.0.12"
-   "Microsoft.EntityFrameworkCore" Version="7.0.12"
-   "Microsoft.EntityFrameworkCore.Design" Version="7.0.12">
-   "Microsoft.Extensions.DependencyInjection" Version="7.0.0"
-   "Swashbuckle.AspNetCore" Version="6.5.0"
-   "FluentValidation.AspNetCore" Version="11.3.0"
-   "itext7.pdfhtml" Version="5.0.1"
-   "Pomelo.EntityFrameworkCore.MySql" Version="7.0.0"
