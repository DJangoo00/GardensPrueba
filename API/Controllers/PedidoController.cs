using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.Dtos;
using Domain.Interfaces;
using Domain.Entities;

namespace API.Controllers
{
    public class PedidoController : BaseApiController
    {
        private readonly IUnitOfWork unitofwork;
        private readonly IMapper mapper;

        public PedidoController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this.unitofwork = unitofwork;
            this.mapper = mapper;
        }

        //Inicio de los controladores v1.0
        [HttpGet]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> Get()
        {
            var entidad = await unitofwork.Pedidos.GetAllAsync();
            return mapper.Map<List<PedidoDto>>(entidad);
        }

        [HttpGet("{id}")]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PedidoDto>> Get(int id)
        {
            var entidad = await unitofwork.Pedidos.GetByIdAsync(id);
            if (entidad == null)
            {
                return NotFound();
            }
            return this.mapper.Map<PedidoDto>(entidad);
        }
        [HttpPost]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pedido>> Post(PedidoDto entidadDto)
        {
            var entidad = this.mapper.Map<Pedido>(entidadDto);
            this.unitofwork.Pedidos.Add(entidad);
            await unitofwork.SaveAsync();
            if (entidad == null)
            {
                return BadRequest();
            }
            entidadDto.Id = entidad.Id;
            return CreatedAtAction(nameof(Post), new { id = entidadDto.Id }, entidadDto);
        }

        [HttpPut("{id}")]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<PedidoDto>> Put(int id, [FromBody] PedidoDto entidadDto)
        {
            if (entidadDto == null)
            {
                return NotFound();
            }
            var entidad = this.mapper.Map<Pedido>(entidadDto);
            unitofwork.Pedidos.Update(entidad);
            await unitofwork.SaveAsync();
            return entidadDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var entidad = await unitofwork.Pedidos.GetByIdAsync(id);
            if (entidad == null)
            {
                return NotFound();
            }
            unitofwork.Pedidos.Remove(entidad);
            await unitofwork.SaveAsync();
            return NoContent();
        }

        //metodos especificos
        [HttpGet("GetC1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<object>>> GetC1()
        {
            var entidad = await unitofwork.Pedidos.GetC1();
            return mapper.Map<List<object>>(entidad);
        }
    }
}