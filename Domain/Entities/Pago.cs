using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Pago : BaseEntityStr
{
    public int IdCliente { get; set; }
    public string FormaPago { get; set; }
    //public string IdTransaccion { get; set; }
    public DateOnly FechaPago { get; set; }
    public decimal Total { get; set; }
    public virtual Cliente IdClienteNavigation { get; set; }
}
