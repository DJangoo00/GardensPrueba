using Domain.Entities;

namespace Domain.Interfaces;

public interface IUnitOfWork
{
    //Main Entities
    ICliente Clientes {get;}
    IDetallePedido DetallesPedidos  {get;}
    IEmpleado Empleados {get;}
    IGamaProducto GamasProductos {get;}
    IOficina Oficinas {get;}
    IPago Pagos {get;}
    IPedido Pedidos {get;}
    IProducto Productos {get;}
    Task<int> SaveAsync();
}
