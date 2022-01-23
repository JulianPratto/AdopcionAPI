using AdopcionAPI.Models;
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
        private readonly ApplicationDbContext context;

        public CentroController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Centro>>> Get()
        {
            var centros = await context.Centros.ToListAsync(); //Select * From Centros;
            return centros;
        }

        [HttpGet("{id}")]
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
        public async Task<ActionResult> Post([FromBody] Centro centro)
        {
            var existeCentroConElMismoNombre = await context.Centros.AnyAsync(x => x.Nombre == centro.Nombre);
            if (existeCentroConElMismoNombre)
            {
                return BadRequest($"Existe un centro con el nombre {centro.Nombre}");

            }

            context.Add(centro);
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
    }
}
