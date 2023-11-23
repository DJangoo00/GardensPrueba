using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace App.Repository;

public class DetallePedidoRepository : GenericRepositoryC<DetallePedido>, IDetallePedido
{
    private readonly ApiContext _context;
    public DetallePedidoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    //override a metodos genericos
    public override async Task<IEnumerable<DetallePedido>> GetAllAsync()
    {
        return await _context.DetallesPedidos
            .ToListAsync();
    }

    public override async Task<DetallePedido> GetByIdAsync(int idI, string idS)
    {
        return await _context.DetallesPedidos
            .FirstOrDefaultAsync(p =>  p.IdPedido == idI && p.IdProducto == idS);
    }

    //Demas Metodos
}