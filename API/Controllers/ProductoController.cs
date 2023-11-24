using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.Dtos;
using Domain.Interfaces;
using Domain.Entities;

namespace API.Controllers
{
    public class ProductoController : BaseApiController
    {
        private readonly IUnitOfWork unitofwork;
        private readonly IMapper mapper;

        public ProductoController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this.unitofwork = unitofwork;
            this.mapper = mapper;
        }

        //Inicio de los controladores v1.0
        [HttpGet]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
        {
            var entidad = await unitofwork.Productos.GetAllAsync();
            return mapper.Map<List<ProductoDto>>(entidad);
        }

        [HttpGet("{id}")]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDto>> Get(string id)
        {
            var entidad = await unitofwork.Productos.GetByIdAsync(id);
            if (entidad == null)
            {
                return NotFound();
            }
            return this.mapper.Map<ProductoDto>(entidad);
        }
        [HttpPost]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> Post(ProductoDto entidadDto)
        {
            var entidad = this.mapper.Map<Producto>(entidadDto);
            this.unitofwork.Productos.Add(entidad);
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

        public async Task<ActionResult<ProductoDto>> Put(int id, [FromBody] ProductoDto entidadDto)
        {
            if (entidadDto == null)
            {
                return NotFound();
            }
            var entidad = this.mapper.Map<Producto>(entidadDto);
            unitofwork.Productos.Update(entidad);
            await unitofwork.SaveAsync();
            return entidadDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var entidad = await unitofwork.Productos.GetByIdAsync(id);
            if (entidad == null)
            {
                return NotFound();
            }
            unitofwork.Productos.Remove(entidad);
            await unitofwork.SaveAsync();
            return NoContent();
        }

        [HttpGet("GetC4")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<object>>> GetC4()
        {
            var entidad = await unitofwork.Productos.GetC4();
            return mapper.Map<List<object>>(entidad);
        }

        [HttpGet("GetC5")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<object>>> GetC5()
        {
            var entidad = await unitofwork.Productos.GetC5();
            return mapper.Map<List<object>>(entidad);
        }

        [HttpGet("GetC6")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<object>>> GetC6()
        {
            var entidad = await unitofwork.Productos.GetC6();
            return mapper.Map<List<object>>(entidad);
        }

        [HttpGet("GetC10")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<object>>> GetC10()
        {
            var entidad = await unitofwork.Productos.GetC10();
            return mapper.Map<List<object>>(entidad);
        }
    }
}