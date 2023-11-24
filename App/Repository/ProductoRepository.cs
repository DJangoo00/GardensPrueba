using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace App.Repository;

public class ProductoRepository : GenericRepositoryStr<Producto>, IProducto
{
    private readonly ApiContext _context;
    public ProductoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    //override a metodos genericos
    public override async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _context.Productos
            .ToListAsync();
    }

    public override async Task<Producto> GetByIdAsync(string id)
    {
        return await _context.Productos
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    //consultas
    public async Task<IEnumerable<object>> GetC4()
    {
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
    }

    public async Task<IEnumerable<object>> GetC5()
    {
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
    }
    public async Task<IEnumerable<object>> GetC6()
    {
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
    }
    public async Task<IEnumerable<object>> GetC10()
    {
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
    }
}