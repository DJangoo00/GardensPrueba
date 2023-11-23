using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.Dtos;
using Domain.Interfaces;
using Domain.Entities;

namespace API.Controllers;
public class GamaProductoController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public GamaProductoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    //Inicio de los controladores v1.0
    [HttpGet]
    //[MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GamaProductoDto>>> Get()
    {
        var entidad = await unitofwork.GamasProductos.GetAllAsync();
        return mapper.Map<List<GamaProductoDto>>(entidad);
    }

    [HttpGet("{id}")]
    //[MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GamaProductoDto>> Get(string id)
    {
        var entidad = await unitofwork.GamasProductos.GetByIdAsync(id);
        if (entidad == null)
        {
            return NotFound();
        }
        return this.mapper.Map<GamaProductoDto>(entidad);
    }
    [HttpPost]
    //[MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GamaProducto>> Post(GamaProductoDto entidadDto)
    {
        var entidad = this.mapper.Map<GamaProducto>(entidadDto);
        this.unitofwork.GamasProductos.Add(entidad);
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

    public async Task<ActionResult<GamaProductoDto>> Put(int id, [FromBody] GamaProductoDto entidadDto)
    {
        if (entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<GamaProducto>(entidadDto);
        unitofwork.GamasProductos.Update(entidad);
        await unitofwork.SaveAsync();
        return entidadDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var entidad = await unitofwork.GamasProductos.GetByIdAsync(id);
        if (entidad == null)
        {
            return NotFound();
        }
        unitofwork.GamasProductos.Remove(entidad);
        await unitofwork.SaveAsync();
        return NoContent();
    }
    //especificos
}