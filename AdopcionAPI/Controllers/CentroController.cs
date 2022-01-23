using AdopcionAPI.DTO;
using AdopcionAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdopcionAPI.Controllers
{
    [ApiController]
    [Route("api/centros")]
    public class CentroController : ControllerBase
    {
        private readonly ApplicationDbContext context; //campo a usar para contexto
        private readonly IMapper mapper; //campo a usar para mapear

        public CentroController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Centro>>> Get()
        {
            var centros = await context.Centros.ToListAsync(); //Select * From Centros;
            return centros;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Centro>> Get(int id)
        {
            var centro = await context.Centros.FirstOrDefaultAsync(centroDB=>centroDB.Id==id);
            if(centro == null)
            {
                return NotFound();
            }
            return centro;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CentroCreacionDTO centroCreacionDTO)
        {
            var existeCentroConElMismoNombre = await context.Centros.AnyAsync(x => x.Nombre == centroCreacionDTO.Nombre);
            if (existeCentroConElMismoNombre)
            {
                return BadRequest($"Existe un centro con el nombre {centroCreacionDTO.Nombre}");
            }
            /*
            *Al usar un objeto DTO no se puede mandar al contexto(contrxt.Add()) este dato centroCreacionDTO.Nombre
            para solventar este problema se crea la variable centro = new Centro, quien si es aceptado
            por context.Add() entonces se pasan los datos recibidos desde la BD por crentroCreacionDTO.Nombre a Nombre
            igualmente con la direccion y el objeto centro ahora contiene ambos datos que se pasaran a la api
            en context.Add(centro);

            ESTE PROCESO SE CONOCE COMO MAPEO O MAPEAR, MAPPING
            */
            /*
            var centro = new Centro
            {
                Nombre = centroCreacionDTO.Nombre,
                Direccion = centroCreacionDTO.Direccion
            };//MAPEO
            */

          //  variable              destino         origen
            var centro = mapper.Map<Centro>(centroCreacionDTO);
            context.Add(centro);//PROCESO
            await context.SaveChangesAsync();

            return Ok();

        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id,[FromBody]Centro centro)
        {
            var existeCentro = await context.Centros.AnyAsync(centroDB => centroDB.Id == id);
            if (!existeCentro)
            {
                return NotFound();
            }

            centro.Id = id;
            context.Entry(centro).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existeCentro = await context.Centros.AnyAsync(centroDB => centroDB.Id == id);
            if(!existeCentro)
            {
                return NotFound();
            }
            context.Remove(new Centro() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<Centro>>>Get(string nombre)
        {
            var centros = await context.Centros.Where(centroDB => centroDB.Nombre.Contains(nombre)).ToListAsync();
            return centros;
        }
    }
}
