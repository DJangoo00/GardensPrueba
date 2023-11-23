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
}