using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopcionAPI.DTO
{
    public class MascotaCreacionDTO
    {
        public string Nombre { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public int CentroId { get; set; }
    }
}
