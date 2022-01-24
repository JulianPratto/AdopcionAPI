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
    public class MascotaController: ControllerBase
    {
        private readonly ApplicationDbContext context; //campo a usar para contexto
        private readonly IMapper mapper; //campo a usar para mapear

        public MascotaController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }



        [HttpPost]
        public async Task<ActionResult> Post([FromBody]MascotaCreacionDTO mascotaCreacionDTO)
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
    }
}
