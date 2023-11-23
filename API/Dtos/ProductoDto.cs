using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos;
    public class ProductoDto : BaseEntityStr
{
    public string Nombre { get; set; }
    public string Gama { get; set; }
    public string Dimensiones { get; set; }
    public string Proveedor { get; set; }
    public string Descripcion { get; set; }
    public short CantidadEnStock { get; set; }
    public decimal PrecioVenta { get; set; }
    public decimal? PrecioProveedor { get; set; }
    public virtual GamaProducto GamaNavigation { get; set; }
}
