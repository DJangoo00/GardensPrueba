using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos;
    public class PagoDto : BaseEntityStr
{
    public int IdCliente { get; set; }
    public string FormaPago { get; set; }
    //public string IdTransaccion { get; set; }
    public DateOnly FechaPago { get; set; }
    public decimal Total { get; set; }
    public virtual ClienteDto IdClienteNavigation { get; set; }
}
