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
}