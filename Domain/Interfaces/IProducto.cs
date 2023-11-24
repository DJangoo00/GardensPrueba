using Domain.Entities;
namespace Domain.Interfaces
{
    public interface IProducto : IGenericRepositoryStr<Producto>
    {
        Task<IEnumerable<object>> GetC4();
        Task<IEnumerable<object>> GetC5();
        Task<IEnumerable<object>> GetC6();
        Task<IEnumerable<object>> GetC10();
    }
}