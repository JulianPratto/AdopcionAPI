using AdopcionAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdopcionAPI
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {

        }

        //configurar todos los modelos del proyecto que necesitemos pegar con DB
        //Asocia el modelo centro con la relacion centros
        //public DbSet<Modelo> Tabla
        public DbSet<Centro> Centros { get; set; }
        //public DbSet<Mascota> Mascotas { get; set; }
    }
}


