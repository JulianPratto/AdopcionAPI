using AdopcionAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdopcionAPI.DTO
{
    public class MascotaConsultaConCentroDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public int CentroId { get; set; }

        public Centro Centro { get; set; }
    }
}
