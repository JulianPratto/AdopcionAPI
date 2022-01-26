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
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext context; //campo a usar para contexto
        private readonly IMapper mapper; //campo a usar para mapear

        public UsuarioController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            var usuarios = await context.Usuarios.ToListAsync();
            return usuarios;
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(usuarioDB => usuarioDB.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuarioCreacionDTO usuarioCreacionDTO)
        {
            var usuario = mapper.Map<Usuario>(usuarioCreacionDTO);
            context.Add(usuario);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult>Put(int id,[FromBody]UsuarioCreacionDTO usuarioCreacionDTO)
        {
            var existeUsuario = await context.Usuarios.AnyAsync(usuarioDB => usuarioDB.Id == id);
            if (!existeUsuario)
            {
                return BadRequest($"No existe usuario con ID: {id}");
            }
            var usuario = mapper.Map<Usuario>(usuarioCreacionDTO);
            usuario.Id = id;
            context.Entry(usuario).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existeUsuario = await context.Usuarios.AnyAsync(usuarioDB => usuarioDB.Id == id);
            if (!existeUsuario)
            {
                return NotFound();
            }
            context.Remove(new Usuario() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
