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
}