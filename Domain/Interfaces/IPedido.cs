using Domain.Entities;
namespace Domain.Interfaces
{
    public interface IPedido : IGenericRepository<Pedido>
    {
        Task<IEnumerable<object>> GetC1();
    }
}