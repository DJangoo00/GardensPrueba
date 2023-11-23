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

**Endpoint**: `http://localhost:5051/api/Cliente/GetC1`


#### 2. Devuelve el nombre de los clientes que no hayan hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina a la que pertenece el representante.

**Endpoint**: `http://localhost:5051/api/Pedido/GetC2`


#### 3. Devuelve las oficinas donde no trabajan ninguno de los empleados que hayan sido los representantes de ventas de algún cliente que haya realizado la compra de algún producto de la gama Frutales.

**Endpoint**: `http://localhost:5051/api/Pago/GetC3`


#### 4. Devuelve un listado de los 20 productos más vendidos y el número total de unidades que se han vendido de cada uno. El listado deberá estar ordenado por el número total de unidades vendidas.

**Endpoint**: `http://localhost:5051/api/Pedido/GetC4`


#### 5. Lista las ventas totales de los productos que hayan facturado más de 3000 euros. Se mostrará el nombre, unidades vendidas, total facturado y total facturado con impuestos (21% IVA).

**Endpoint**: `http://localhost:5051/api/Pedido/GetC5`


#### 6. Devuelve el nombre del producto del que se han vendido más unidades. (Tenga en cuenta que tendrá que calcular cuál es el número total de unidades que se han vendido de cada producto a partir de los datos de la tabla detalle_pedido).

**Endpoint**: `http://localhost:5051/api/Pedido/GetC6`


#### 7. Devuelve el listado de clientes indicando el nombre del cliente y cuántos pedidos ha realizado. Tenga en cuenta que pueden existir clientes que no han realizado ningún pedido.

**Endpoint**: `http://localhost:5051/api/Pedido/GetC7`


#### 8. Devuelve el listado de clientes donde aparezca el nombre del cliente, el nombre y primer apellido de su representante de ventas y la ciudad donde está su oficina.

**Endpoint**: `http://localhost:5051/api/Pago/GetC8`


#### 9. Devuelve un listado con los datos de los empleados que no tienen clientes asociados y el nombre de su jefe asociado.

**Endpoint**: `http://localhost:5051/api/Pago/GetC9`

#### 10. Devuelve un listado de los productos que nunca han aparecido en un pedido. El resultado debe mostrar el nombre, la descripción y la imagen del producto..

**Endpoint**: `http://localhost:5051/api/Pago/GetC9`



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
