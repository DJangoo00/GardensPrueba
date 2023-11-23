using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace App.Repository;

public class GamaProductoRepository : GenericRepositoryStr<GamaProducto>, IGamaProducto
{
    private readonly ApiContext _context;
    public GamaProductoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    //override a metodos genericos
    public override async Task<IEnumerable<GamaProducto>> GetAllAsync()
    {
        return await _context.GamasProductos
            .ToListAsync();
    }

    public override async Task<GamaProducto> GetByIdAsync(string id)
    {
        return await _context.GamasProductos
            .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}