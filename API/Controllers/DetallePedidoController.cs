using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.Dtos;
using Domain.Interfaces;
using Domain.Entities;

namespace API.Controllers
{
    public class DetallePedidoController : BaseApiController
    {
        private readonly IUnitOfWork unitofwork;
        private readonly IMapper mapper;

        public DetallePedidoController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this.unitofwork = unitofwork;
            this.mapper = mapper;
        }

        //Inicio de los controladores v1.0
        [HttpGet]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DetallePedidoDto>>> Get()
        {
            var entidad = await unitofwork.DetallesPedidos.GetAllAsync();
            return mapper.Map<List<DetallePedidoDto>>(entidad);
        }

        [HttpGet("{id}")]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DetallePedidoDto>> Get(int idI, string idS)
        {
            var entidad = await unitofwork.DetallesPedidos.GetByIdAsync(idI, idS);
            if (entidad == null)
            {
                return NotFound();
            }
            return this.mapper.Map<DetallePedidoDto>(entidad);
        }
        [HttpPost]
    //[MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetallePedido>> Post(DetallePedidoDto entidadDto)
    {
        var entidad = this.mapper.Map<DetallePedido>(entidadDto);
        this.unitofwork.DetallesPedidos.Add(entidad);
        await unitofwork.SaveAsync();
        if (entidad == null)
        {
            return BadRequest();
        }
        entidadDto.IdPedido = entidad.IdPedido;
        entidadDto.IdProducto = entidad.IdProducto;
        return CreatedAtAction(nameof(Post), new { idI = entidadDto.IdPedido, idS = entidadDto.IdProducto}, entidadDto);
    }

    [HttpPut("{id}")]
    //[MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<DetallePedidoDto>> Put(int id, [FromBody] DetallePedidoDto entidadDto)
    {
        if (entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<DetallePedido>(entidadDto);
        unitofwork.DetallesPedidos.Update(entidad);
        await unitofwork.SaveAsync();
        return entidadDto;
    }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int idI, string idS)
        {
            var entidad = await unitofwork.DetallesPedidos.GetByIdAsync(idI, idS);
            if (entidad == null)
            {
                return NotFound();
            }
            unitofwork.DetallesPedidos.Remove(entidad);
            await unitofwork.SaveAsync();
            return NoContent();
        }

        //especificos
    }
}