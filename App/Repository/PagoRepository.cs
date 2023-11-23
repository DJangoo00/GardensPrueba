using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace App.Repository;

public class PagoRepository : GenericRepositoryStr<Pago>, IPago
{
    private readonly ApiContext _context;
    public PagoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    //override a metodos genericos
    public override async Task<IEnumerable<Pago>> GetAllAsync()
    {
        return await _context.Pagos
            .ToListAsync();
    }

    public override async Task<Pago> GetByIdAsync(string id)
    {
        return await _context.Pagos
            .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}