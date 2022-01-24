using AdopcionAPI.DTO;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdopcionAPI.Models;

namespace AdopcionAPI.Controllers
{
    [ApiController]
    [Route("api/mascotas")]
    public class MascotaController : ControllerBase
    {
        private readonly ApplicationDbContext context; //campo a usar para contexto
        private readonly IMapper mapper; //campo a usar para mapear

        public MascotaController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<MascotaDTO>>> Get()
        {
            var mascotas = await context.Mascotas.ToListAsync();

            return mapper.Map<List<MascotaDTO>>(mascotas);
        }
        [HttpGet("{id:int}", Name = "obtenerMascota")]
        public async Task<ActionResult<MascotaDTO>> Get(int id)
        {
            var mascota = await context.Mascotas.FirstOrDefaultAsync(mascotaDB => mascotaDB.Id == id);
            if (mascota == null)
            {
                return NotFound();
            }
            return mapper.Map<MascotaDTO>(mascota);
        }
        [HttpGet("{id:int}/centro", Name = "obtenerMascotaConCentro")]
        public async Task<ActionResult<MascotaConsultaConCentroDTO>> Get(int id, [FromQuery] bool conCentro = true)
        {
            var mascota = await context.Mascotas.FirstOrDefaultAsync(mascotaDB => mascotaDB.Id == id);
            if (mascota == null)
            {
                return NotFound();
            }
            var mascotaConsultaDTO = mapper.Map<MascotaConsultaConCentroDTO>(mascota);
            var centro = new Centro();
            if (conCentro)
            {
                //buscar la informacion del centro de la mascota por medio del id del centro de la mascota contra el id en la tabla centros y sacar el detalle de dicho centro

                centro = await context.Centros.FirstOrDefaultAsync(centroDB => centroDB.Id == mascota.CentroId);
                mascotaConsultaDTO.Centro = centro;
            }
            return mascotaConsultaDTO;
        }




        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MascotaCreacionDTO mascotaCreacionDTO)
        {
            var existeCentro = await context.Centros.AnyAsync(centroDB => centroDB.Id == mascotaCreacionDTO.CentroId);

            if (!existeCentro)
            {
                return BadRequest($"No existe centro con ID: {mascotaCreacionDTO.CentroId}");
            }
            var mascota = mapper.Map<Mascota>(mascotaCreacionDTO);
            context.Add(mascota);
            await context.SaveChangesAsync();
            return Ok();

        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult>Put(int id,[FromBody]MascotaCreacionDTO mascotaCreacionDTO)
        {
            var existeMascota = await context.Mascotas.AnyAsync(mascotaDB => mascotaDB.Id == id);
            if (!existeMascota)
            {
                return BadRequest($"No existe la mascota con ID: {id}");
            }
            var existeCentro = await context.Centros.AnyAsync(centroDB => centroDB.Id == mascotaCreacionDTO.CentroId);
            if (!existeCentro)
            {
                return BadRequest($"No existe un centro de adopcion con ID: {mascotaCreacionDTO.CentroId}");
            }
            var mascota = mapper.Map<Mascota>(mascotaCreacionDTO);
            mascota.Id = id;
            context.Entry(mascota).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existeMascota = await context.Mascotas.AnyAsync(mascotaDB => mascotaDB.Id == id);
            if (!existeMascota)
            {
                return NotFound();
            }
            context.Remove(new Mascota() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
