using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.Dtos;
using Domain.Interfaces;
using Domain.Entities;

namespace API.Controllers
{
    public class EmpleadoController : BaseApiController
    {
        private readonly IUnitOfWork unitofwork;
        private readonly IMapper mapper;

        public EmpleadoController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this.unitofwork = unitofwork;
            this.mapper = mapper;
        }

        //Inicio de los controladores v1.0
        [HttpGet]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmpleadoDto>>> Get()
        {
            var entidad = await unitofwork.Empleados.GetAllAsync();
            return mapper.Map<List<EmpleadoDto>>(entidad);
        }

        [HttpGet("{id}")]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmpleadoDto>> Get(int id)
        {
            var entidad = await unitofwork.Empleados.GetByIdAsync(id);
            if (entidad == null)
            {
                return NotFound();
            }
            return this.mapper.Map<EmpleadoDto>(entidad);
        }
        [HttpPost]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Empleado>> Post(EmpleadoDto entidadDto)
        {
            var entidad = this.mapper.Map<Empleado>(entidadDto);
            this.unitofwork.Empleados.Add(entidad);
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

        public async Task<ActionResult<EmpleadoDto>> Put(int id, [FromBody] EmpleadoDto entidadDto)
        {
            if (entidadDto == null)
            {
                return NotFound();
            }
            var entidad = this.mapper.Map<Empleado>(entidadDto);
            unitofwork.Empleados.Update(entidad);
            await unitofwork.SaveAsync();
            return entidadDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var entidad = await unitofwork.Empleados.GetByIdAsync(id);
            if (entidad == null)
            {
                return NotFound();
            }
            unitofwork.Empleados.Remove(entidad);
            await unitofwork.SaveAsync();
            return NoContent();
        }

        //metodos especificos
    }
}