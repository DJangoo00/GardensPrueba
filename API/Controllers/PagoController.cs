using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.Dtos;
using Domain.Interfaces;
using Domain.Entities;

namespace API.Controllers
{
    public class PagoController : BaseApiController
    {
        private readonly IUnitOfWork unitofwork;
        private readonly IMapper mapper;

        public PagoController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this.unitofwork = unitofwork;
            this.mapper = mapper;
        }

        //Inicio de los controladores v1.0
        [HttpGet]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PagoDto>>> Get()
        {
            var entidad = await unitofwork.Pagos.GetAllAsync();
            return mapper.Map<List<PagoDto>>(entidad);
        }

        [HttpGet("{id}")]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagoDto>> Get(string id)
        {
            var entidad = await unitofwork.Pagos.GetByIdAsync(id);
            if (entidad == null)
            {
                return NotFound();
            }
            return this.mapper.Map<PagoDto>(entidad);
        }
        [HttpPost]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pago>> Post(PagoDto entidadDto)
        {
            var entidad = this.mapper.Map<Pago>(entidadDto);
            this.unitofwork.Pagos.Add(entidad);
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

        public async Task<ActionResult<PagoDto>> Put(int id, [FromBody] PagoDto entidadDto)
        {
            if (entidadDto == null)
            {
                return NotFound();
            }
            var entidad = this.mapper.Map<Pago>(entidadDto);
            unitofwork.Pagos.Update(entidad);
            await unitofwork.SaveAsync();
            return entidadDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var entidad = await unitofwork.Pagos.GetByIdAsync(id);
            if (entidad == null)
            {
                return NotFound();
            }
            unitofwork.Pagos.Remove(entidad);
            await unitofwork.SaveAsync();
            return NoContent();
        }

        //metodos especificos

        
    }
}