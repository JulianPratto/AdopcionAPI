using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdopcionAPI.Models
{
    public class AdopcionCreacionDTO
    {
        public DateTime FechaAdopcion { get; set; }

        public int MascotaId { get; set; }

        public int UserId { get; set; }
    }
}
