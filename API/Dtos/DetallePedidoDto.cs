using Domain.Entities;

namespace API.Dtos;

public class DetallePedidoDto : BaseEntityC
{
    public int Cantidad { get; set; }
    public decimal PrecioUnidad { get; set; }
    public short NumeroLinea { get; set; }
    public virtual PedidoDto IdPedidoNavigation { get; set; }
    public virtual ProductoDto IdProductoNavigation { get; set; }
}
