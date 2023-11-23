using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos;
    public class EmpleadoDto : BaseEntity
{
    public string Nombre { get; set; }
    public string Apellidol { get; set; }
    public string Apellido2 { get; set; }
    public string Extension { get; set; }
    public string Email { get; set; }
    public string IdOficina { get; set; }
    public int? IdJefe { get; set; }
    public string Puesto { get; set; }
    public virtual EmpleadoDto IdJefeNavigation { get; set; }
    public virtual OficinaDto IdOficinaNavigation { get; set; }
}