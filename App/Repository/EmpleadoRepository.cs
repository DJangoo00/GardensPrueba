using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace App.Repository;

public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleado
{
    private readonly ApiContext _context;
    public EmpleadoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    //override a metodos genericos
    public override async Task<IEnumerable<Empleado>> GetAllAsync()
    {
        return await _context.Empleados
            .ToListAsync();
    }

    public override async Task<Empleado> GetByIdAsync(int id)
    {
        return await _context.Empleados
            .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    //consultas

    public async Task<IEnumerable<object>> GetC9()
    {
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
    }
}