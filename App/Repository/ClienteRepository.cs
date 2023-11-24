using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;
using System.Security.Cryptography.X509Certificates;

namespace App.Repository;

public class ClienteRepository : GenericRepository<Cliente>, ICliente
{
    private readonly ApiContext _context;
    public ClienteRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    //override a metodos genericos
    public override async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Clientes
            .ToListAsync();
    }

    public override async Task<Cliente> GetByIdAsync(int id)
    {
        return await _context.Clientes
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    //Demas Metodos
    //consultas
    public async Task<IEnumerable<object>> GetC2()
    {
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
    }

    public async Task<IEnumerable<object>> GetC7()
    {
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
    }
    public async Task<IEnumerable<object>> GetC8()
    {
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
    }
}