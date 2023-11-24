using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace App.Repository;

public class PedidoRepository : GenericRepository<Pedido>, IPedido
{
    private readonly ApiContext _context;
    public PedidoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    //override a metodos genericos
    public override async Task<IEnumerable<Pedido>> GetAllAsync()
    {
        return await _context.Pedidos
            .ToListAsync();
    }

    public override async Task<Pedido> GetByIdAsync(int id)
    {
        return await _context.Pedidos
            .FirstOrDefaultAsync(p =>  p.Id == id);
    }

    //consultas
    public async Task<IEnumerable<object>> GetC1()
    {
        var result = await (
            from p in _context.Pedidos
            where p.FechaEsperada != p.FechaEntrega
            select new
            {
                IdPedido = p.Id,
                IdCliente = p.IdCliente,
                FechaEsperada = p.FechaEsperada,
                FechaEntrega = p.FechaEntrega,
            }
        ).ToListAsync();
        return result;
    }
    
}