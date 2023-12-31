using App.Repository;
using Domain.Interfaces;
using Persistence;

namespace App.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApiContext _context;
    //variables repos
    private ClienteRepository _clientes;
    private DetallePedidoRepository _detallespedidos;
    private EmpleadoRepository _empleados;
    private GamaProductoRepository _gamaspedidos;
    private OficinaRepository _oficinas;
    private PagoRepository _pagos;
    private PedidoRepository _pedidos;
    private ProductoRepository _productos;
    public UnitOfWork(ApiContext context)
    {
        _context = context;
    }
    //repos
    public ICliente Clientes
    {
        get
        {
            if (_clientes == null)
            {
                _clientes = new ClienteRepository(_context);
            }
            return _clientes;
        }
    }
    public IDetallePedido DetallesPedidos
    {
        get
        {
            if (_detallespedidos == null)
            {
                _detallespedidos = new DetallePedidoRepository(_context);
            }
            return _detallespedidos;
        }
    }
    public IEmpleado Empleados
    {
        get
        {
            if (_empleados == null)
            {
                _empleados = new EmpleadoRepository(_context);
            }
            return _empleados;
        }
    }
    public IGamaProducto GamasProductos
    {
        get
        {
            if (_gamaspedidos == null)
            {
                _gamaspedidos = new GamaProductoRepository(_context);
            }
            return _gamaspedidos;
        }
    }
    public IOficina Oficinas
    {
        get
        {
            if (_oficinas == null)
            {
                _oficinas = new OficinaRepository(_context);
            }
            return _oficinas;
        }
    }
    public IPago Pagos
    {
        get
        {
            if (_pagos == null)
            {
                _pagos = new PagoRepository(_context);
            }
            return _pagos;
        }
    }
    public IPedido Pedidos
    {
        get
        {
            if (_pedidos == null)
            {
                _pedidos = new PedidoRepository(_context);
            }
            return _pedidos;
        }
    }
    public IProducto Productos
    {
        get
        {
            if (_productos == null)
            {
                _productos = new ProductoRepository(_context);
            }
            return _productos;
        }
    }

    public int Save()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}