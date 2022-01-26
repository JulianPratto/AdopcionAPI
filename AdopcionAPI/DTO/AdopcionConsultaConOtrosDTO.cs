using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdopcionAPI.Models;

namespace AdopcionAPI.Models
{
    public class AdopcionConsultaConOtrosDTO
    {

        public int Id { get; set; }
        public DateTime FechaAdopcion { get; set; }

        public int MascotaId { get; set; }

        public int UserId { get; set; }

        public Usuario Usuario { get; set; }

        public Mascota Mascota { get; set; }
    }
}
