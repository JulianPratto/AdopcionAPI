using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdopcionAPI.DTO
{
    public class CentroCreacionDTO
    {
        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }
        //public List<Mascota> Mascotas { get; set; }
        [StringLength(100)]
        public string Direccion { get; set; }

    }
}
