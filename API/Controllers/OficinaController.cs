using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.Dtos;
using Domain.Interfaces;
using Domain.Entities;

namespace API.Controllers
{
    public class OficinaController : BaseApiController
    {
        private readonly IUnitOfWork unitofwork;
        private readonly IMapper mapper;

        public OficinaController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this.unitofwork = unitofwork;
            this.mapper = mapper;
        }

        //Inicio de los controladores v1.0
        [HttpGet]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OficinaDto>>> Get()
        {
            var entidad = await unitofwork.Oficinas.GetAllAsync();
            return mapper.Map<List<OficinaDto>>(entidad);
        }

        [HttpGet("{id}")]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OficinaDto>> Get(string id)
        {
            var entidad = await unitofwork.Oficinas.GetByIdAsync(id);
            if (entidad == null)
            {
                return NotFound();
            }
            return this.mapper.Map<OficinaDto>(entidad);
        }
        [HttpPost]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Oficina>> Post(OficinaDto entidadDto)
        {
            var entidad = this.mapper.Map<Oficina>(entidadDto);
            this.unitofwork.Oficinas.Add(entidad);
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

        public async Task<ActionResult<OficinaDto>> Put(int id, [FromBody] OficinaDto entidadDto)
        {
            if (entidadDto == null)
            {
                return NotFound();
            }
            var entidad = this.mapper.Map<Oficina>(entidadDto);
            unitofwork.Oficinas.Update(entidad);
            await unitofwork.SaveAsync();
            return entidadDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var entidad = await unitofwork.Oficinas.GetByIdAsync(id);
            if (entidad == null)
            {
                return NotFound();
            }
            unitofwork.Oficinas.Remove(entidad);
            await unitofwork.SaveAsync();
            return NoContent();
        }
        
        //especificos
        [HttpGet("GetC3")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OficinaDto>>> GetC3()
        {
            var entidad = await unitofwork.Oficinas.GetC3();
            return mapper.Map<List<OficinaDto>>(entidad);
        }
    }
}