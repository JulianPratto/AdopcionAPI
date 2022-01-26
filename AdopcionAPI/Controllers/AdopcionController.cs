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
    [Route("api/adopcion")]
    public class AdopcionController : ControllerBase
    {
        private readonly ApplicationDbContext context; //campo a usar para contexto
        private readonly IMapper mapper; //campo a usar para mapear

        public AdopcionController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Adopcion>>> Get()
        {
            var adopcion = await context.Adopciones.ToListAsync();
            return adopcion;
        }

        [HttpGet("{id:int}/otros", Name = "obtenerAdopcionConOtros")]
        public async Task<ActionResult<AdopcionConsultaConOtrosDTO>> Get(int id, [FromQuery] bool conMascotayUsuario = true)
        {
            var adopcion = await context.Adopciones.FirstOrDefaultAsync(adopcionDB => adopcionDB.Id == id);
            if (adopcion == null)
            {
                return NotFound();
            }
            var adopcionConsultaConOtrosDTO = mapper.Map<AdopcionConsultaConOtrosDTO>(adopcion);
            var usuario = new Usuario();
            var mascota = new Mascota();
            if (conMascotayUsuario)
            {
                usuario = await context.Usuarios.FirstOrDefaultAsync(usuarioDB => usuarioDB.Id == adopcion.UserId);
                mascota = await context.Mascotas.FirstOrDefaultAsync(mascotaDB => mascotaDB.Id == adopcion.MascotaId);
                adopcionConsultaConOtrosDTO.Usuario = usuario;
                adopcionConsultaConOtrosDTO.Mascota = mascota;
            }
            return adopcionConsultaConOtrosDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AdopcionCreacionDTO adopcionCreacionDTO)
        {
            var existeUsuario = await context.Usuarios.AnyAsync(usuarioDB => usuarioDB.Id == adopcionCreacionDTO.UserId);
            var existeMascota = await context.Mascotas.AnyAsync(mascotaDB => mascotaDB.Id == adopcionCreacionDTO.MascotaId);
            if (!existeUsuario)
            {
                return BadRequest($"No existe usuario con ID: {adopcionCreacionDTO.UserId}");
            }
            if (!existeMascota)
            {
                return BadRequest($"No existe mascota con ID: {adopcionCreacionDTO.MascotaId}");
            }
            var adopcion = mapper.Map<Adopcion>(adopcionCreacionDTO);
            context.Add(adopcion);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult>Put(int id,[FromBody] AdopcionCreacionDTO adopcionCreacionDTO)
        {
            var existeAdopcion = await context.Adopciones.AnyAsync(adopcionDB => adopcionDB.Id == id);
            var existeUsuario = await context.Usuarios.AnyAsync(usuarioDB => usuarioDB.Id == adopcionCreacionDTO.UserId);
            var existeMascota = await context.Mascotas.AnyAsync(mascotaDB => mascotaDB.Id == adopcionCreacionDTO.MascotaId);
            if (!existeAdopcion)
            {
                return BadRequest($"No existe la mascota con ID: {id}");
            }
            if (!existeUsuario)
            {
                return BadRequest($"No existe usuario con ID: {adopcionCreacionDTO.UserId}");
            }
            if (!existeMascota)
            {
                return BadRequest($"No existe mascota con ID: {adopcionCreacionDTO.MascotaId}");
            }
            var adopcion = mapper.Map<Adopcion>(adopcionCreacionDTO);
            adopcion.Id = id;
            context.Entry(adopcion).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existeAdopcion = await context.Adopciones.AnyAsync(adopcionDB => adopcionDB.Id == id);
            if (!existeAdopcion)
            {
                return NotFound();
            }
            context.Remove(new Adopcion() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
