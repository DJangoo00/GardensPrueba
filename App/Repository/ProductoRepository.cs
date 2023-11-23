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
            .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}