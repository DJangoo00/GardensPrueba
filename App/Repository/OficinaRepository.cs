using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace App.Repository;

public class OficinaRepository : GenericRepositoryStr<Oficina>, IOficina
{
    private readonly ApiContext _context;
    public OficinaRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    //override a metodos genericos
    public override async Task<IEnumerable<Oficina>> GetAllAsync()
    {
        return await _context.Oficinas
            .ToListAsync();
    }

    public override async Task<Oficina> GetByIdAsync(string id)
    {
        return await _context.Oficinas
            .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    //Demas Metodos
    public async Task<IEnumerable<object>> GetC3()
    {
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
    }
}