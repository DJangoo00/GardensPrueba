using Domain.Entities;
namespace Domain.Interfaces
{
    public interface IEmpleado : IGenericRepository<Empleado>
    {
        Task<IEnumerable<object>> GetC9();
    }
}