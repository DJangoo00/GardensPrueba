using Domain.Entities;
namespace Domain.Interfaces
{
    public interface IOficina : IGenericRepositoryStr<Oficina>
    {
        Task<IEnumerable<object>> GetC3();
    }
}