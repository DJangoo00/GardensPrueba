using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICliente : IGenericRepository<Cliente>
    {
        Task<IEnumerable<object>> GetC2();
        Task<IEnumerable<object>> GetC7();
        Task<IEnumerable<object>> GetC8();
    }
}
